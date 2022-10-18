using System;
using System.Collections.Generic;
using System.Linq;
using RineaR.BeatABit.Environments;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    [CreateAssetMenu(menuName = "Beat a Badge/Create Flag", fileName = "Flag", order = 0)]
    public class Flag : ScriptableObject
    {
        public enum TriggerType
        {
            None,
            ClearStages,
            Never,
        }

        public TriggerType type;
        public List<string> targetStageKeys;

        [Min(1)]
        public int clearBorder;

        public bool IsSet(ApplicationSession applicationSession)
        {
            switch (type)
            {
                case TriggerType.None:
                    return true;
                case TriggerType.Never:
                    return false;
                case TriggerType.ClearStages:
                    var clearCount = targetStageKeys
                        .Count(targetStage => applicationSession.recordTable.IsCleared(targetStage));
                    return clearCount >= clearBorder;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}