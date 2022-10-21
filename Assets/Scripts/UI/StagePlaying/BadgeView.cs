using DG.Tweening;
using RineaR.BeatABit.Core;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RineaR.BeatABit.UI.StagePlaying
{
    public class BadgeView : MonoBehaviour
    {
        public AthleticSystem system;
        public int beatNumber;

        public TMP_Text beatNumberText;
        public Image iconImage;

        private void Awake()
        {
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

        private void Update()
        {
            if (system.IsReady)
            {
                iconImage.sprite = system.chartPlayer.arrange.BadgeOf(beatNumber)?.icon;
                iconImage.enabled = iconImage.sprite;
                beatNumberText.text = beatNumber.ToString("D2");
                beatNumberText.enabled = true;
            }
            else
            {
                iconImage.enabled = false;
                beatNumberText.enabled = false;
            }
        }

        private void OnSystemReady()
        {
            system.chartPlayer.ObserveEveryValueChanged(chartPlayer => chartPlayer.beat).Subscribe(OnBeatChanged)
                .AddTo(this);
        }

        private void OnBeatChanged(int current)
        {
            if (current == beatNumber)
                iconImage.rectTransform.DOSizeDelta(Vector2.one * 60, 0.2f);
            else
                iconImage.rectTransform.DOSizeDelta(Vector2.one * 48, 0.2f);
        }
    }
}