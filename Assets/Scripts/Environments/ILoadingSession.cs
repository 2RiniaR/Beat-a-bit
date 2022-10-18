using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.BeatABit.Environments
{
    public interface ILoadingSession
    {
        IEnumerable<float> Progresses { get; }
        UniTask LoadAsync(CancellationToken token);
        UniTask UnloadAsync(CancellationToken token);
    }
}