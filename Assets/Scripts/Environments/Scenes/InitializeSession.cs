using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.BeatABit.Environments.Scenes
{
    public class InitializeSession : ILoadingSession
    {
        public IEnumerable<float> Progresses => ArraySegment<float>.Empty;

        public async UniTask LoadAsync(CancellationToken token)
        {
            await CacheRepository.Current.Load(token);
            await SaveRepository.Current.Load(token);
            await CacheRepository.Current.Save(token);
            await SaveRepository.Current.Save(token);
        }

        public UniTask UnloadAsync(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }
    }
}