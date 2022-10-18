using System;
using RineaR.BeatABit.Core.BadgeEffects;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    [CreateAssetMenu(menuName = "Beat a Badge/Create Badge", fileName = "Badge", order = 0)]
    public class Badge : ScriptableObject
    {
        public enum EffectType
        {
            Boost,
            Fly,
            Freeze,
            Light,
            Heavy,
            Hide,
            Stop,
        }

        public EffectType effectType;
        public Color color;
        public Sprite icon;
        public string description;
        public Flag unlockFlag;

        private IBadgeEffect _effect;

        public void EnableEffect(Transform target)
        {
            if (_effect != null) return;

            _effect = effectType switch
            {
                EffectType.Boost => new BoostEffect(target),
                EffectType.Fly => new FlyEffect(target),
                EffectType.Freeze => new FreezeEffect(target),
                EffectType.Light => new LightEffect(target),
                EffectType.Heavy => new HeavyEffect(target),
                EffectType.Hide => new HideEffect(target),
                EffectType.Stop => new StopEffect(target),
                _ => throw new ArgumentOutOfRangeException(),
            };

            _effect.EnableEffect();
        }

        public void DisableEffect()
        {
            if (_effect == null) return;

            _effect.DisableEffect();
            _effect = null;
        }
    }
}