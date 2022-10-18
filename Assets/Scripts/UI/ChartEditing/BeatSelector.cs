using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RineaR.BeatABit.UI.ChartEditing
{
    public class BeatSelector : MonoBehaviour, ISelectHandler
    {
        [Header("References")]
        public List<Graphic> colorSyncGraphics;

        public TMP_Text beatNumberText;
        public Image iconImage;

        [Header("Animation")]
        public string editingBoolean = "isEditing";

        [Header("Properties")]
        public int beatNumber;

        [Header("State")]
        public BoolReactiveProperty isEditing;

        /// <summary>
        ///     同じ GameObject にアタッチされている Animator
        /// </summary>
        private Animator _animator;

        /// <summary>
        ///     同じ GameObject にアタッチされている Button
        /// </summary>
        private Button _button;

        /// <summary>
        ///     所属中の ChartEditor
        /// </summary>
        private ChartEditor _chartEditor;

        private void Start()
        {
            _chartEditor = GetComponentInParent<ChartEditor>();
            _animator = GetComponent<Animator>();
            _button = GetComponent<Button>();
            if (_button) _button.OnClickAsObservable().Subscribe(_ => Submit()).AddTo(this);

            isEditing.Subscribe(x =>
            {
                if (_animator) _animator.SetBool(editingBoolean, x);
            }).AddTo(this);
        }

        private void Update()
        {
            var beat = _chartEditor.ChartEdit.Chart.BeatOf(beatNumber);
            var bit = _chartEditor.ChartEdit.BitOf(beatNumber);

            gameObject.SetActive(beat != null);

            if (beatNumberText) beatNumberText.text = beatNumber.ToString();

            if (_button) _button.interactable = beat != null && !beat.locked && !_chartEditor.selecting;

            if (iconImage)
            {
                iconImage.enabled = bit;
                iconImage.sprite = bit ? bit.icon : null;
            }

            var color = bit ? bit.color : Color.white;
            foreach (var graphic in colorSyncGraphics)
            {
                graphic.color = color;
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            _chartEditor.UpdateDescription(_chartEditor.ChartEdit.BitOf(beatNumber));
        }

        public void Submit()
        {
            _chartEditor.EditBeat(this);
        }
    }
}