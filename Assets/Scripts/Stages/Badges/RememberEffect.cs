using System;
using RineaR.BeatABit.Core;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Badges
{
    [RequireComponent(typeof(AthleticSystem))]
    public class RememberEffect : MonoBehaviour
    {
        private AthleticSystem _system;

        private void Awake()
        {
            _system = GetComponent<AthleticSystem>() ?? throw new NullReferenceException();
        }
    }
}