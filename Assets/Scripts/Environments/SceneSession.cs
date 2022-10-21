using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RineaR.BeatABit.Environments.Scenes;
using RineaR.BeatABit.General;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace RineaR.BeatABit.Environments
{
    public class SceneSession
    {
        private SceneInstance _loadedScene;
        private AsyncOperationHandle<SceneInstance> _loadingSceneHandle;

        public SceneSession(SceneType sceneType)
        {
            SceneType = sceneType;
            LoadingSession = sceneType switch
            {
                SceneType.Common => throw new ArgumentException("シーン Common のセッションは作成できません。"),
                SceneType.Initialize => new InitializeSession(),
                SceneType.StagePlay => new StagePlaySession(),
                SceneType.StageSelect => null,
                SceneType.Title => null,
                _ => throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null),
            };
        }

        public SceneType SceneType { get; }
        [CanBeNull] private ILoadingSession LoadingSession { get; }

        public float GetLoadingProgress()
        {
            if (LoadingSession == null) return _loadingSceneHandle.GetPercentSafety();

            var progresses = new List<float>(LoadingSession.Progresses) { _loadingSceneHandle.GetPercentSafety() };
            return progresses.Average();
        }

        public async UniTask LoadAsync(CancellationToken token)
        {
            if (LoadingSession != null)
            {
                await LoadingSession.LoadAsync(token);
                if (token.IsCancellationRequested) return;
            }

            _loadingSceneHandle = Addressables.LoadSceneAsync(SceneNameProvider.GetName(SceneType),
                LoadSceneMode.Additive, false);
            _loadedScene = await _loadingSceneHandle.ToUniTask(cancellationToken: token);
        }

        public async UniTask ActivateSceneAsync(CancellationToken token)
        {
            await _loadedScene.ActivateAsync().ToUniTask(cancellationToken: token);
        }

        public async UniTask UnloadAsync(CancellationToken token)
        {
            if (_loadingSceneHandle.IsValid())
                await Addressables.UnloadSceneAsync(_loadingSceneHandle).ToUniTask(cancellationToken: token);
            else
            {
                var name = SceneNameProvider.GetName(SceneType);
                await SceneManager.UnloadSceneAsync(name);
                token.ThrowIfCancellationRequested();
            }

            if (LoadingSession != null) await LoadingSession.UnloadAsync(token);
        }
    }
}