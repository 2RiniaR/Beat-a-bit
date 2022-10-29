using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.BeatABit.Helpers
{
    public static class MonoBehaviourExtension
    {
        public static async UniTask DelayWhenEnabled(this MonoBehaviour source, TimeSpan delayTime,
            CancellationToken cancellationToken = default)
        {
            var current = TimeSpan.Zero;
            while (true)
            {
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);
                await UniTask.WaitUntil(() => source.isActiveAndEnabled, cancellationToken: cancellationToken);
                current = current.Add(TimeSpan.FromSeconds(Time.deltaTime));
                if (current >= delayTime) return;
            }
        }
    }
}