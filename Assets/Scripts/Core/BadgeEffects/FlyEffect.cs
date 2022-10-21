using System;
using UnityEngine;

namespace RineaR.BeatABit.Core.BadgeEffects
{
    /// <summary>
    ///     効果中、プレイヤーが空中ジャンプ可能になる。
    /// </summary>
    [RequireComponent(typeof(AthleticSystem))]
    public class FlyEffect : MonoBehaviour
    {
        private bool _restore;
        private AthleticSystem _system;

        private void Awake()
        {
            _system = GetComponent<AthleticSystem>() ?? throw new NullReferenceException();
        }

        private void Update()
        {
            _system.bit.fly = true;
        }

        private void OnEnable()
        {
            _restore = _system.bit.fly;
        }

        private void OnDisable()
        {
            _system.bit.fly = _restore;
        }
    }
}