using UnityEngine;
using UnityEngine.EventSystems;

namespace RineaR.BeatABit.General
{
    public class FocusTracer : MonoBehaviour
    {
        public GameObject selecting;

        private void Update()
        {
            selecting = EventSystem.current.currentSelectedGameObject;
        }
    }
}