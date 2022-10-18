﻿using UnityEngine;

namespace RineaR.BeatABit.Core.BadgeEffects
{
    public class HeavyEffect : IBadgeEffect
    {
        public HeavyEffect(Transform target)
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