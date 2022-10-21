using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RineaR.BeatABit.General;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using ThreadPriority = UnityEngine.ThreadPriority;

namespace RineaR.BeatABit.Environments
{
    public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
    {
        public ProgressView defaultTransitionView;
        private readonly Dictionary<SceneType, SceneSession> _sessions = new();

        [NotNull]
        private SceneSession GetSession(SceneType sceneType)
        {
            return _sessions.TryGetValue(sceneType, out var session)
                ? session
                : throw new InvalidOperationException($"シーン {sceneType} は読み込まれていません。");
        }

        private bool HasSessionExists(SceneType sceneType)
        {
            return _sessions.ContainsKey(sceneType);
        }

        [NotNull]
        private SceneSession CreateSession(SceneType sceneType)
        {
            if (HasSessionExists(sceneType)) throw new InvalidOperationException($"シーン {sceneType} は既に読み込まれています。");

            var newSession = new SceneSession(sceneType);
            _sessions.Add(sceneType, newSession);
            return newSession;
        }

        private void RemoveSession(SceneType sceneType)
        {
            if (!HasSessionExists(sceneType)) throw new InvalidOperationException($"シーン {sceneType} は読み込まれていません。");
            _sessions.Remove(sceneType);
        }

        public void ChangeScene(SceneType next, SceneType previous, [CanBeNull] ProgressView view = null)
        {
            ChangeSceneAsync(next, previous, view ? view : defaultTransitionView, CancellationToken.None).Forget();
        }

        private async UniTask ChangeSceneAsync(SceneType next, SceneType previous, [CanBeNull] ProgressView view,
            CancellationToken token)
        {
            if (view && !view.gameObject.activeInHierarchy) view = Instantiate(view);
            var nextSession = CreateSession(next);
            var updatingProgressTask = Observable.EveryUpdate().Subscribe(_ =>
            {
                if (view) view.SetProgress(nextSession.GetLoadingProgress());
            }).AddTo(token);

            Application.backgroundLoadingPriority = ThreadPriority.High;
            await (nextSession.LoadAsync(token), view ? view.OpenAsync(token) : UniTask.CompletedTask);
            await nextSession.ActivateSceneAsync(token);
            Application.backgroundLoadingPriority = ThreadPriority.Low;
            await updatingProgressTask.DisposeAsync();
            await (UnloadSceneAsync(previous, token), view ? view.CloseAsync(token) : UniTask.CompletedTask);
            if (view) Destroy(view.gameObject);
        }

        public void ReloadScene(SceneType sceneType)
        {
            UniTask.Create(async () =>
            {
                await UnloadSceneAsync(sceneType, CancellationToken.None);
                var session = CreateSession(sceneType);
                await session.LoadAsync(CancellationToken.None);
                await session.ActivateSceneAsync(CancellationToken.None);
            }).Forget();
        }

        public void LoadScene(SceneType sceneType)
        {
            UniTask.Create(async () =>
            {
                var session = CreateSession(sceneType);
                await session.LoadAsync(CancellationToken.None);
                await session.ActivateSceneAsync(CancellationToken.None);
            }).Forget();
        }

        private async UniTask UnloadSceneAsync(SceneType sceneType, CancellationToken token)
        {
            if (HasSessionExists(sceneType))
            {
                var session = GetSession(sceneType);
                await session.UnloadAsync(CancellationToken.None);
                RemoveSession(sceneType);
            }
            else
                await SceneManager.UnloadSceneAsync(SceneNameProvider.GetName(sceneType));
        }
    }
}