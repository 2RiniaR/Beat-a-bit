using System;
using System.Collections.Generic;
using RineaR.BeatABit.Helpers;

namespace RineaR.BeatABit.Core
{
    [Serializable]
    public class ChartArrange
    {
        public List<Badge> badges;

        public Badge BadgeOf(int beatNumber)
        {
            return badges.TryAccess(beatNumber - 1);
        }
    }
}