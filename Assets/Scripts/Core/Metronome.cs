using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    /// <summary>
    ///     メトロノーム。一定の拍と拍子でイベントを発行し続ける。
    /// </summary>
    public class Metronome : MonoBehaviour
    {
        [Header("Properties")]
        [Min(float.Epsilon)]
        [Tooltip("拍子のBPM。")]
        public float bpm = 120;

        [Min(0)]
        [Tooltip("再生開始時に、1小節目を開始するまでにカウントする拍子の数。")]
        public int preTickCount = 4;

        [Min(1)]
        [Tooltip("小節内にある拍子の数。")]
        public int beatCount = 4;

        public AudioClip beatSound;
        public AudioClip tickSound;

        [Header("States")]
        public int tickCount;

        public float tickProgress;

        private readonly Subject<Unit> _onBeat = new();
        private readonly Subject<int> _onTick = new();

        private float Interval => 60 / bpm;

        private void Start()
        {
            OnBeatAsObservable().Subscribe(_ => PlayBeatSound()).AddTo(this);
            OnTickAsObservable().Subscribe(_ => PlayTickSound()).AddTo(this);
        }

        private void OnEnable()
        {
            LoopAsync(this.GetCancellationTokenOnDestroy()).Forget(e => { });
        }

        /// <summary>
        ///     小節が開始したタイミングで呼び出される。
        /// </summary>
        public IObservable<Unit> OnBeatAsObservable()
        {
            return _onBeat;
        }

        /// <summary>
        ///     拍子のタイミングで呼び出される。小節内の何拍子目か（0から開始）がメッセージとして渡される。また、1小節目が開始する前のカウント時にも呼び出される。
        /// </summary>
        public IObservable<int> OnTickAsObservable()
        {
            return _onTick;
        }

        public void PlayBeatSound()
        {
            if (beatSound) SEManager.Instance.Play(beatSound);
        }

        public void PlayTickSound()
        {
            if (tickSound) SEManager.Instance.Play(tickSound);
        }

        private async UniTask LoopAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
            if (!isActiveAndEnabled) return;

            for (var i = 0; i < preTickCount; i++)
            {
                tickCount = i + 1;
                _onTick.OnNext(tickCount);
                await WaitForNextTick(token);
                if (!isActiveAndEnabled) return;
            }

            while (true)
            {
                _onBeat.OnNext(Unit.Default);

                for (var i = 0; i < Mathf.Max(1, beatCount); i++)
                {
                    tickCount = i + 1;
                    _onTick.OnNext(i + 1);
                    await WaitForNextTick(token);
                    if (!isActiveAndEnabled) return;
                }
            }
        }

        private async UniTask WaitForNextTick(CancellationToken token)
        {
            var duration = 0f;
            tickProgress = 0;

            while (true)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, token);
                if (!isActiveAndEnabled) return;

                duration += Time.deltaTime;
                tickProgress = Mathf.Clamp01(duration / Interval);
                if (duration >= Interval) break;
            }
        }
    }
}