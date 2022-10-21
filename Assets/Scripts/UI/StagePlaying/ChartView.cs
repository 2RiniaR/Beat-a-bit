using System;
using DG.Tweening;
using RineaR.BeatABit.Core;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.UI.StagePlaying
{
    public class ChartView : MonoBehaviour
    {
        public AthleticSystem system;
        public float slideDuration;
        public float slideDistance;
        public BadgeView elementPrefab;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = transform as RectTransform ?? throw new InvalidOperationException();
            system ??= GetComponentInParent<AthleticSystem>();
        }

        private void Reset()
        {
            system = GetComponentInParent<AthleticSystem>();
        }

        private void Start()
        {
            system.OnReadyAsObservable().Subscribe(_ => OnSystemReady()).AddTo(this);
        }

        private void OnSystemReady()
        {
            system.chartPlayer.ObserveEveryValueChanged(chartPlayer => chartPlayer.beat).Subscribe(Focus).AddTo(this);
        }

        private void Focus(int beatNumber)
        {
            _rectTransform.DOAnchorPosX(-slideDistance * beatNumber, slideDuration).SetEase(Ease.InOutQuint);
        }
    }
}