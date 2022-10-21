using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.BeatABit.Environments.Data;
using RineaR.BeatABit.General;
using UnityEngine;

namespace RineaR.BeatABit.Environments
{
    public class CacheRepository : SingletonMonoBehaviour<CacheRepository>
    {
        public string sourceFilePath = "cache.json";
        public CacheData source;
        public string SourceFileFullPath => Path.Join(Application.persistentDataPath, sourceFilePath);

        public async UniTask Load(CancellationToken token)
        {
            var data = ScriptableObject.CreateInstance<SaveData>();

            if (source)
                JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(source), data);
            else if (File.Exists(SourceFileFullPath))
            {
                using var streamReader = new StreamReader(SourceFileFullPath);
                var raw = await streamReader.ReadToEndAsync().ConfigureAwait(true);
                token.ThrowIfCancellationRequested();
                JsonUtility.FromJsonOverwrite(raw, data);
            }

            ApplicationSession.Current.recordTable = data.recordTable;

            Destroy(data);
        }

        public async UniTask Save(CancellationToken token)
        {
            var data = ScriptableObject.CreateInstance<SaveData>();
            data.recordTable = ApplicationSession.Current.recordTable;

            await using var streamWriter = new StreamWriter(SourceFileFullPath);
            token.ThrowIfCancellationRequested();
            await streamWriter.WriteAsync(JsonUtility.ToJson(data));
            token.ThrowIfCancellationRequested();
            await streamWriter.FlushAsync();

            Destroy(data);
        }
    }
}