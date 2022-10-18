using System.Collections.Generic;
using RineaR.BeatABit.Helpers;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    [CreateAssetMenu(menuName = "Beat a Badge/Create Chart", fileName = "Chart", order = 0)]
    public class Chart : ScriptableObject
    {
        public List<Beat> beats;

        public Beat BeatOf(int beatNumber)
        {
            return beats.TryAccess(beatNumber - 1);
        }
    }
}