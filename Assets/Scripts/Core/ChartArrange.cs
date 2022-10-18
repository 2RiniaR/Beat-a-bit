using System;
using System.Collections.Generic;
using RineaR.BeatABit.Helpers;

namespace RineaR.BeatABit.Core
{
    [Serializable]
    public class ChartArrange
    {
        public List<Badge> bits;

        public Badge BitOf(int beatNumber)
        {
            return bits.TryAccess(beatNumber - 1);
        }
    }
}