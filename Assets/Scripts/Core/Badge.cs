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
    }
}