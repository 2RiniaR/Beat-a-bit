using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    public class ChartPlayer : MonoBehaviour
    {
        [Header("References")]
        public Metronome metronome;

        public Chart playing;
        public ChartArrange arrange;
        public Transform effectTarget;

        [Header("States")]
        public int beat;

        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable()
        {
            metronome.enabled = true;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.RegisterRaiseCancelOnDestroy(this);
            LoopAsync(_cancellationTokenSource.Token).Forget();
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTask LoopAsync(CancellationToken token)
        {
            beat = 0;

            while (true)
            {
                await metronome.OnBeatAsObservable().First().ToUniTask(cancellationToken: token);
                if (token.IsCancellationRequested) return;

                beat++;
                UpdateEffect(arrange.BitOf(beat - 1), arrange.BitOf(beat));
            }
        }

        private void UpdateEffect([CanBeNull] Badge previous, [CanBeNull] Badge next)
        {
            if (!effectTarget) return;
            if (previous) previous.DisableEffect();
            if (next) next.EnableEffect(effectTarget);
        }

        public bool IsReady()
        {
            return metronome && playing && arrange != null;
        }
    }
}