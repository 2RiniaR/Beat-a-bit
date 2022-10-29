using System;
using RineaR.BeatABit.Stages.Motions;
using UnityEngine;

namespace RineaR.BeatABit.Stages
{
    /// <summary>
    ///     ランナー。プレイヤーが操作するキャラクター。
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Runner : MonoBehaviour
    {
        [Header("Parameters")]
        public float jumpMinHeight;

        public float jumpMaxHeight;
        public float moveSpeed;

        public bool canMove = true;
        public bool fly;

        private DashMotion _dashMotion;
        private GroundSensor _groundSensor;
        private AdjustableJumpMotion _jumpMotion;

        private Rigidbody2D _rigidbody2D;
        public Life Life { get; private set; }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>() ?? throw new NullReferenceException();

            _groundSensor = new GroundSensor
            {
                Actor = _rigidbody2D,
            };

            _jumpMotion = new AdjustableJumpMotion
            {
                Actor = _rigidbody2D,
                MinHeight = jumpMinHeight,
                MaxHeight = jumpMaxHeight,
            };
            _jumpMotion.Sync(this);

            _dashMotion = new DashMotion
            {
                Actor = _rigidbody2D,
                MaxVelocityMagnitude = moveSpeed,
                AccelerationTime = 0.1f,
                DecelerationTime = 0.1f,
                Input = 0f,
            };
            _dashMotion.Sync(this);

            Life = new Life
            {
                Max = 2,
                InvincibleDurationOnDamage = 2,
            };
            Life.Sync(this);
        }

        private void Start()
        {
            Life.Initialize();
        }

        private void Update()
        {
            _rigidbody2D.bodyType = canMove ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;

            _jumpMotion.MinHeight = jumpMinHeight;
            jumpMinHeight = _jumpMotion.MinHeight;

            _jumpMotion.MaxHeight = jumpMaxHeight;
            jumpMaxHeight = _jumpMotion.MaxHeight;

            _dashMotion.MaxVelocityMagnitude = moveSpeed;
            moveSpeed = _dashMotion.MaxVelocityMagnitude;
        }

        public void Move(float x)
        {
            _dashMotion.Input = x;
        }

        public void TriggerJump()
        {
            if (fly || _groundSensor.IsGround()) _jumpMotion.Trigger();
        }

        public void FinishJump()
        {
            _jumpMotion.Finish();
        }

        public void StartAct()
        {
        }

        public void FinishAct()
        {
        }

        public void Damage()
        {
            Debug.Log("Damage!");
        }

        public void Kill()
        {
        }
    }
}