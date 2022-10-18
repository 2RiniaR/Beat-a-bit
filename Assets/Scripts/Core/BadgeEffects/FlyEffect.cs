using UnityEngine;

namespace RineaR.BeatABit.Core.BadgeEffects
{
    /// <summary>
    ///     効果中、プレイヤーが空中ジャンプ可能になる。
    /// </summary>
    public class FlyEffect : IBadgeEffect
    {
        private bool _original;
        private Player _player;

        public FlyEffect(Transform target)
        {
            Target = target;
        }

        public Transform Target { get; }

        public void EnableEffect()
        {
            _player = Target.GetComponentInChildren<Player>();
            if (_player == null) return;

            _original = _player.fly;
            _player.fly = true;
        }

        public void DisableEffect()
        {
            if (_player == null) return;

            _player.fly = _original;
        }
    }
}