using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RineaR.BeatABit.Core
{
    [CreateAssetMenu(menuName = "Beat a Badge/Create Stage", fileName = "Stage", order = 0)]
    public class Stage : ScriptableObject
    {
        public AssetReferenceGameObject athletic;
        public Chart chart;
        public Flag unlockFlag;
    }
}