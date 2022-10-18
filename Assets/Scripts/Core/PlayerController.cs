using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RineaR.BeatABit.Core
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour, MainControls.IRobotActions
    {
        private MainControls _input;

        [CanBeNull]
        private Player _player;

        private void Awake()
        {
            _input = new MainControls();
            _input.Robot.SetCallbacks(this);
        }

        private void Start()
        {
            _player = GetComponent<Player>() ?? throw new NullReferenceException();
        }

        private void FixedUpdate()
        {
            if (_player != null) _player.Move(_input.Robot.Move.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void OnDestroy()
        {
            _input.Dispose();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // do nothing
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed || _player == null) return;

            _player.Jump();
        }

        public void OnAct(InputAction.CallbackContext context)
        {
            if (_player == null) return;

            if (context.performed) _player.StartAct();

            if (context.canceled) _player.FinishAct();
        }
    }
}