using UnityEngine;

namespace RineaR.BeatABit.Core.BadgeEffects
{
    public class HideEffect : IBadgeEffect
    {
        private bool _original;
        private Player _player;

        public HideEffect(Transform target)
        {
            Target = target;
        }

        public Transform Target { get; }

        public void EnableEffect()
        {
            _player = Target.GetComponentInChildren<Player>();
            if (_player == null) return;

            _original = _player.hide;
            _player.hide = true;
        }

        public void DisableEffect()
        {
            if (_player == null) return;

            _player.hide = _original;
        }
    }
}