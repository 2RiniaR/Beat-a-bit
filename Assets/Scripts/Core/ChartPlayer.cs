using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RineaR.BeatABit.Core.BadgeEffects;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    public class ChartPlayer : MonoBehaviour
    {
        [Header("References")]
        public AthleticSystem system;

        public Metronome metronome;

        public Chart playing;
        public ChartArrange arrange;

        [Header("States")]
        public int beat;

        public Component currentEffect;

        private void OnEnable()
        {
            metronome.enabled = true;
            LoopAsync(this.GetCancellationTokenOnDestroy()).Forget(e => { });
        }

        private async UniTask LoopAsync(CancellationToken token)
        {
            beat = 0;

            while (true)
            {
                await metronome.OnBeatAsObservable().First().ToUniTask(cancellationToken: token);
                if (!isActiveAndEnabled) return;
                beat++;
                UpdateEffect(arrange.BadgeOf(beat));
            }
        }

        private void UpdateEffect([CanBeNull] Badge next)
        {
            if (!system) return;
            if (currentEffect) Destroy(currentEffect);

            if (!next) return;
            currentEffect = system.gameObject.AddComponent(next.effectType switch
            {
                Badge.EffectType.Boost => typeof(BoostEffect),
                Badge.EffectType.Fly => typeof(FlyEffect),
                Badge.EffectType.Freeze => typeof(FreezeEffect),
                Badge.EffectType.Light => typeof(LightEffect),
                Badge.EffectType.Heavy => typeof(HeavyEffect),
                Badge.EffectType.Hide => typeof(HideEffect),
                Badge.EffectType.Stop => typeof(StopEffect),
                _ => throw new ArgumentOutOfRangeException(),
            });
        }
    }
}