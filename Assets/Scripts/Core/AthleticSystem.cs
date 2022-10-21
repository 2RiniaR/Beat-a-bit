using System;
using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    public class AthleticSystem : MonoBehaviour
    {
        [Tooltip("準備完了のために必須。nullの場合は、自動的に子から探して割り当てる。Prefabが渡されていた場合、Start()でInstantiateされる。")]
        public Athletic athletic;

        [Tooltip("準備完了のために必須。nullの場合は、自動的に子から探して割り当てる。Prefabが渡されていた場合、Start()でInstantiateされる。")]
        public Bit bit;

        [Tooltip("準備完了のために必須。nullの場合は、自動的に子から探して割り当てる。Prefabが渡されていた場合、Start()でInstantiateされる。")]
        public ChartPlayer chartPlayer;

        [Tooltip("準備完了のために必須。")]
        public Player player;

        [Tooltip("ここにカメラを設定すると、カメラがBitを追尾するようになる。nullでも準備完了となる。")]
        public CinemachineVirtualCamera virtualCamera;

        private readonly AsyncSubject<Unit> _onReady = new();
        public bool IsReady => _onReady.IsCompleted;

        private void Awake()
        {
            _onReady.AddTo(this);

            athletic ??= GetComponentInChildren<Athletic>();
            if (athletic && !athletic.gameObject.activeInHierarchy)
                athletic = Instantiate(athletic, transform);
            if (!athletic) throw new InvalidOperationException("Athleticが存在しません！");

            bit ??= GetComponentInChildren<Bit>();

            chartPlayer ??= GetComponentInChildren<ChartPlayer>();
            if (chartPlayer && !chartPlayer.gameObject.activeInHierarchy)
                chartPlayer = Instantiate(chartPlayer, transform);
            if (!chartPlayer) throw new InvalidOperationException("ChartPlayerが存在しません！");

            player ??= FindObjectOfType<Player>();
            if (player && !player.gameObject.activeInHierarchy)
                player = Instantiate(player, transform);
            if (!player) throw new InvalidOperationException("Playerが存在しません！");
        }

        private void Start()
        {
            if (!bit) throw new InvalidOperationException("Bitが存在しません！");

            if (!bit.gameObject.activeInHierarchy)
            {
                if (!athletic.entrance)
                    throw new InvalidOperationException("Bitを生成しようとしましたが、Athletic.entranceが存在しません！");
                bit = Instantiate(bit, athletic.entrance.position, Quaternion.identity, transform);
            }

            chartPlayer.enabled = true;
            chartPlayer.system = this;
            player.bitController.bit = bit;
            if (virtualCamera) virtualCamera.Follow = bit.transform;

            _onReady.OnNext(Unit.Default);
            _onReady.OnCompleted();

            CountReadyAsync(this.GetCancellationTokenOnDestroy()).Forget(e => { });
        }

        /// <summary>
        ///     準備が出来たら呼び出される。
        /// </summary>
        public IObservable<Unit> OnReadyAsObservable()
        {
            return _onReady;
        }

        private async UniTask CountReadyAsync(CancellationToken token)
        {
            bit.canMove = false;
            player.bitController.enabled = false;

            await UniTask.WaitUntil(() => chartPlayer.beat >= 1, cancellationToken: token);

            bit.canMove = true;
            player.bitController.enabled = true;
        }
    }
}