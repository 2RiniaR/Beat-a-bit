using System;
using JetBrains.Annotations;

namespace RineaR.BeatABit.Core
{
    [Serializable]
    public class Beat
    {
        [CanBeNull]
        public Badge locked;
    }
}