using System.Collections.Generic;
using RineaR.BeatABit.Core;
using RineaR.BeatABit.Environments;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RineaR.BeatABit.UI.ChartEditing
{
    public class BadgeSetter : MonoBehaviour, ISelectHandler
    {
        [Header("References")]
        public List<Graphic> colorSyncGraphics;

        public Image iconImage;

        [Header("Properties")]
        public Badge badge;

        private Button _button;

        private ChartEditor _chartEditor;

        private void Awake()
        {
            _chartEditor = GetComponentInParent<ChartEditor>();
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            if (_button) _button.OnClickAsObservable().Subscribe(_ => Submit()).AddTo(this);
        }

        private void Update()
        {
            if (_button)
            {
                _button.interactable =
                    _chartEditor.selecting && badge && badge.unlockFlag.IsSet(ApplicationSession.Current);
            }

            if (iconImage)
            {
                iconImage.enabled = badge;
                iconImage.sprite = badge ? badge.icon : null;
            }

            var color = badge ? badge.color : Color.white;
            foreach (var graphic in colorSyncGraphics)
            {
                graphic.color = color;
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            _chartEditor.UpdateDescription(badge);
        }

        public void Submit()
        {
            _chartEditor.SetBadge(this);
        }
    }
}