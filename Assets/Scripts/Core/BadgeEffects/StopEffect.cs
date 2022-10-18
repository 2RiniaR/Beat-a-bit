using UnityEngine;

namespace RineaR.BeatABit.Core.BadgeEffects
{
    public class StopEffect : IBadgeEffect
    {
        public StopEffect(Transform target)
        {
            Target = target;
        }

        public Transform Target { get; }

        public void EnableEffect()
        {
        }

        public void DisableEffect()
        {
        }
    }
}