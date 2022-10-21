using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RineaR.BeatABit.Core
{
    public class BitController : MonoBehaviour, MainControls.IBitActions
    {
        public Bit bit;
        private MainControls _controls;

        private void Awake()
        {
            _controls = new MainControls();
            _controls.Bit.SetCallbacks(this);
            _controls.AddTo(this);
        }

        private void FixedUpdate()
        {
            if (bit) bit.Move(_controls.Bit.Move.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            _controls.Bit.Enable();
        }

        private void OnDisable()
        {
            _controls.Bit.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // do nothing
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed || !bit) return;

            if (context.ReadValueAsButton()) bit.TriggerJump();
            else bit.FinishJump();
        }

        public void OnAct(InputAction.CallbackContext context)
        {
            if (!bit) return;

            if (context.performed) bit.StartAct();

            if (context.canceled) bit.FinishAct();
        }
    }
}