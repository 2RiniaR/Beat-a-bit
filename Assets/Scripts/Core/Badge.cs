using System;
using RineaR.BeatABit.Stages.Badges;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    [CreateAssetMenu(menuName = "Beat a Badge/Create Badge", fileName = "Badge", order = 0)]
    public class Badge : ScriptableObject
    {
        public enum EffectType
        {
            Dash,
            Air,
            Freeze,
            Light,
            Heavy,
            Hide,
            Slow,
            Pick,
            Impact,
            Remember,
            Magnet,
            Shield,
            Grip,
            Minimal,
            Hold,
            Cast,
            Site,
        }

        public EffectType effectType;
        public Color color;
        public Sprite icon;
        public string description;
        public Flag unlockFlag;

        public Component AddEffect(AthleticSystem system)
        {
            return system.gameObject.AddComponent(effectType switch
            {
                EffectType.Dash => typeof(DashEffect),
                EffectType.Air => typeof(AirEffect),
                EffectType.Freeze => typeof(FreezeEffect),
                EffectType.Light => typeof(LightEffect),
                EffectType.Heavy => typeof(HeavyEffect),
                EffectType.Hide => typeof(HideEffect),
                EffectType.Slow => typeof(SlowEffect),
                EffectType.Pick => typeof(PickEffect),
                EffectType.Impact => typeof(ImpactEffect),
                EffectType.Remember => typeof(RememberEffect),
                EffectType.Magnet => typeof(MagnetEffect),
                EffectType.Shield => typeof(ShieldEffect),
                EffectType.Grip => typeof(GripEffect),
                EffectType.Minimal => typeof(MinimalEffect),
                EffectType.Hold => typeof(HoldEffect),
                EffectType.Cast => typeof(CastEffect),
                EffectType.Site => typeof(SiteEffect),
                _ => throw new ArgumentOutOfRangeException(),
            });
        }
    }
}