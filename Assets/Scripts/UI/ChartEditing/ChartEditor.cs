using System;
using RineaR.BeatABit.Core;
using RineaR.BeatABit.General;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RineaR.BeatABit.UI.ChartEditing
{
    public class ChartEditor : MonoBehaviour, MainControls.IChartEditActions
    {
        [Header("References")]
        public TMP_Text descriptionText;

        public Selectable focusOnSelectBeat;
        public Selectable focusOnEditBeat;
        public Button submitButton;
        public SubWindowLayout badgeSelectWindow;

        [Header("State")]
        public BeatSelector selecting;

        [Header("Data")]
        public Chart chart;

        private MainControls _controls;

        private Subject<ChartArrange> _onSubmitted;
        public ChartEdit ChartEdit { get; private set; }

        private void Awake()
        {
            _controls = new MainControls();
            _controls.ChartEdit.SetCallbacks(this);

            _onSubmitted = new Subject<ChartArrange>();
            _onSubmitted.AddTo(this);
        }

        private void Start()
        {
            ChartEdit = new ChartEdit(chart);

            if (focusOnSelectBeat)
            {
                EventSystem.current.firstSelectedGameObject = focusOnSelectBeat.gameObject;
                EventSystem.current.SetSelectedGameObject(focusOnSelectBeat.gameObject);
            }

            if (badgeSelectWindow) badgeSelectWindow.gameObject.SetActive(false);

            if (submitButton) submitButton.OnClickAsObservable().Subscribe(_ => Submit()).AddTo(this);
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _controls.Dispose();
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Submit();
        }

        public IObservable<ChartArrange> OnSubmittedAsObservable()
        {
            return _onSubmitted;
        }

        public void EditBeat(BeatSelector beatSelector)
        {
            if (!beatSelector) return;

            selecting = beatSelector;
            beatSelector.isEditing.Value = true;
            if (badgeSelectWindow)
            {
                badgeSelectWindow.owner = beatSelector.GetComponent<RectTransform>();
                badgeSelectWindow.gameObject.SetActive(true);
            }

            EventSystem.current.SetSelectedGameObject(focusOnEditBeat.gameObject);
        }

        public void SetBadge(BadgeSetter badgeSetter)
        {
            if (!selecting || !badgeSetter) return;

            ChartEdit.Assign(selecting.beatNumber, badgeSetter.badge);
            EventSystem.current.SetSelectedGameObject(selecting.gameObject);
            selecting.isEditing.Value = false;
            selecting = null;

            if (badgeSelectWindow) badgeSelectWindow.gameObject.SetActive(false);
        }

        public void UpdateDescription(Badge badge)
        {
            if (!descriptionText) return;

            if (badge)
            {
                descriptionText.text = badge.description;
                descriptionText.color = badge.color;
            }
            else
                descriptionText.text = null;
        }

        public void Submit()
        {
            _onSubmitted.OnNext(ChartEdit.PublishArrange());
        }
    }
}