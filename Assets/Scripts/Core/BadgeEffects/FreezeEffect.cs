using UnityEngine;

namespace RineaR.BeatABit.Core.BadgeEffects
{
    public class FreezeEffect : IBadgeEffect
    {
        public FreezeEffect(Transform target)
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