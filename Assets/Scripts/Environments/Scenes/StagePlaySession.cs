using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.BeatABit.Core;
using RineaR.BeatABit.General;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RineaR.BeatABit.Environments.Scenes
{
    public class StagePlaySession : ILoadingSession
    {
        private AsyncOperationHandle<GameObject> _loadingAthleticHandle;
        public IEnumerable<float> Progresses => new[] { _loadingAthleticHandle.GetPercentSafety() };

        public async UniTask LoadAsync(CancellationToken token)
        {
            _loadingAthleticHandle = ApplicationSession.Current.stage.athletic.LoadAssetAsync();
            var athleticObject = await _loadingAthleticHandle.ToUniTask(cancellationToken: token);
            ApplicationSession.Current.athletic = athleticObject.GetComponent<Athletic>();
        }

        public UniTask UnloadAsync(CancellationToken token)
        {
            if (_loadingAthleticHandle.IsValid()) Addressables.Release(_loadingAthleticHandle);
            return UniTask.CompletedTask;
        }
    }
}