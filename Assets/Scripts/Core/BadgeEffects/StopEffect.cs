﻿using System;
using UnityEngine;

namespace RineaR.BeatABit.Core.BadgeEffects
{
    [RequireComponent(typeof(AthleticSystem))]
    public class StopEffect : MonoBehaviour
    {
        private AthleticSystem _system;

        private void Awake()
        {
            _system = GetComponent<AthleticSystem>() ?? throw new NullReferenceException();
        }
    }
}