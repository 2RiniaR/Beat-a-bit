using UnityEngine;

namespace RineaR.BeatABit.Core.BadgeEffects
{
    public class LightEffect : IBadgeEffect
    {
        public LightEffect(Transform target)
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