using RineaR.BeatABit.Stages;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RineaR.BeatABit.Core
{
    public class RunnerController : MonoBehaviour, MainControls.IRunnerActions
    {
        public Runner runner;
        private MainControls _controls;

        private void Awake()
        {
            _controls = new MainControls();
            _controls.Runner.SetCallbacks(this);
            _controls.AddTo(this);
        }

        private void FixedUpdate()
        {
            if (runner) runner.Move(_controls.Runner.Move.ReadValue<float>());
        }

        private void OnEnable()
        {
            _controls.Runner.Enable();
        }

        private void OnDisable()
        {
            _controls.Runner.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // do nothing
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed || !runner) return;

            if (context.ReadValueAsButton()) runner.TriggerJump();
            else runner.FinishJump();
        }

        public void OnAct(InputAction.CallbackContext context)
        {
            if (!runner) return;

            if (context.performed) runner.StartAct();

            if (context.canceled) runner.FinishAct();
        }
    }
}