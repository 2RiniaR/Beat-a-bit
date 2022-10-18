using UnityEngine;

namespace RineaR.BeatABit.Core.BadgeEffects
{
    public class BoostEffect : IBadgeEffect
    {
        public BoostEffect(Transform target)
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