using System;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bit : MonoBehaviour
    {
        [Header("References")]
        public Collider2D holdZoneCollider;

        [Header("Properties")]
        public GroundSensor groundSensor;

        public JumpMotion jumpMotion;
        public DashMotion dashMotion;

        [Header("States")]
        [Min(0)]
        public int life;

        public bool canMove = true;
        public bool fly;
        public bool hide;

        private Rigidbody2D _rigidbody2D;
        private int _wallLayerMask;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>() ?? throw new NullReferenceException();
            groundSensor.Rigidbody2D = _rigidbody2D;
            jumpMotion.Rigidbody2D = _rigidbody2D;
            dashMotion.Rigidbody2D = _rigidbody2D;
        }

        private void Update()
        {
            _rigidbody2D.bodyType = canMove ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
        }

        private void FixedUpdate()
        {
            dashMotion.UpdateVelocity();
            jumpMotion.UpdateVelocity();
        }

        public void Move(Vector2 vector)
        {
            dashMotion.input = vector.x;
        }

        public void TriggerJump()
        {
            if (fly || groundSensor.IsGround()) jumpMotion.Trigger();
        }

        public void FinishJump()
        {
            jumpMotion.Finish();
        }

        public void StartAct()
        {
        }

        public void FinishAct()
        {
        }
    }
}