using RineaR.BeatABit.Core;
using RineaR.BeatABit.Environments.Data;
using RineaR.BeatABit.General;

namespace RineaR.BeatABit.Environments
{
    /// <summary>
    ///     起動時のみ保持され、アプリを終了すると破棄されるデータ。
    /// </summary>
    public class ApplicationSession : SingletonMonoBehaviour<ApplicationSession>
    {
        public Stage stage;
        public RecordTable recordTable;
        public Athletic athletic;
    }
}