using System;
using RineaR.BeatABit.Core;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Badges
{
    /// <summary>
    ///     効果中、プレイヤーが空中ジャンプ可能になる。
    /// </summary>
    [RequireComponent(typeof(AthleticSystem))]
    public class AirEffect : MonoBehaviour
    {
        private AthleticSystem _system;

        private void Awake()
        {
            _system = GetComponent<AthleticSystem>() ?? throw new NullReferenceException();
        }

        private void Update()
        {
            _system.runner.fly = true;
        }

        private void OnDisable()
        {
            _system.runner.fly = false;
        }
    }
}