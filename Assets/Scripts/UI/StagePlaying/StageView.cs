using RineaR.BeatABit.Core;
using TMPro;
using UnityEngine;

namespace RineaR.BeatABit.UI.StagePlaying
{
    public class StageView : MonoBehaviour
    {
        public AthleticSystem system;
        public TMP_Text nameText;

        private void Awake()
        {
            system ??= GetComponentInParent<AthleticSystem>();
        }

        private void Reset()
        {
            system = GetComponentInParent<AthleticSystem>();
        }

        private void Update()
        {
            if (system.IsReady)
            {
                nameText.enabled = true;
                nameText.text = system.athletic.displayName;
            }
            else
                nameText.enabled = false;
        }
    }
}