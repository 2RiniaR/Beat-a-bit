using Cinemachine;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    public class AttemptAthleticSystem : MonoBehaviour
    {
        [Header("References")]
        public Athletic athletic;

        public ChartPlayer chartPlayer;
        public Player player;
        public CinemachineVirtualCamera virtualCamera;

        private void Start()
        {
            var existingPlayer = FindObjectOfType<Player>();
            if (existingPlayer) player = existingPlayer;
            if (player && !player.gameObject.activeInHierarchy)
                player = Instantiate(player, athletic.entrance.position, Quaternion.identity, transform);

            if (!player)
            {
                enabled = false;
                return;
            }

            if (virtualCamera) virtualCamera.Follow = player.transform;

            if (chartPlayer) chartPlayer.enabled = true;
        }

        public bool IsReady()
        {
            return chartPlayer && chartPlayer.IsReady();
        }
    }
}