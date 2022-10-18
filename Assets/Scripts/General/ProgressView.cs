using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.BeatABit.Environments;
using TMPro;
using UnityEngine;

namespace RineaR.BeatABit.General
{
    public class ProgressView : MonoBehaviour
    {
        public string openedTrigger = "Opened";
        public string closedTrigger = "Closed";
        public string progressFloat = "Progress";
        public TMP_Text progressText;
        public float openDuration;
        public float closeDuration;
        public Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            CommonLocator.Current.Transfer(gameObject);
        }

        public async UniTask OpenAsync(CancellationToken token)
        {
            if (animator) animator.SetTrigger(openedTrigger);
            await UniTask.Delay(TimeSpan.FromSeconds(openDuration), cancellationToken: token);
        }

        public void SetProgress(float progress)
        {
            if (animator) animator.SetFloat(progressFloat, progress);
            if (progressText) progressText.text = progress.ToString("P0");
        }

        public async UniTask CloseAsync(CancellationToken token)
        {
            if (animator) animator.SetTrigger(closedTrigger);
            await UniTask.Delay(TimeSpan.FromSeconds(closeDuration), cancellationToken: token);
        }
    }
}