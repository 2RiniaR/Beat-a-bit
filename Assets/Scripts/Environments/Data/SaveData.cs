using UnityEngine;

namespace RineaR.BeatABit.Environments.Data
{
    /// <summary>
    ///     永続化される変更可能なデータのうち、ゲームの進行に関わるもの。
    /// </summary>
    [CreateAssetMenu(menuName = "Beat a Badge/Create SaveData", fileName = "SaveData", order = 0)]
    public class SaveData : ScriptableObject
    {
        public RecordTable recordTable;
    }
}