using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RineaR.BeatABit.General
{
    [RequireComponent(typeof(Selectable))]
    public class SelectableCustomizer : MonoBehaviour, IPointerEnterHandler
    {
        private Selectable _selectable;

        private void Start()
        {
            _selectable = GetComponent<Selectable>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_selectable.interactable) _selectable.Select();
        }
    }
}