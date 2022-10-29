using System;
using RineaR.BeatABit.Stages.Motions;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Monsters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hornet : MonoBehaviour
    {
        public Collider2D jumpTriggerArea;
        public float jumpPower;

        private GroundSensor _groundSensor;
        private JumpMotion _jumpMotion;
        private RunnerSensor _jumpSensor;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>() ?? throw new NullReferenceException();

            _groundSensor = new GroundSensor
            {
                Actor = _rigidbody2D,
            };

            _jumpMotion = new JumpMotion
            {
                Actor = _rigidbody2D,
                Height = jumpPower,
            };
            _jumpMotion.Sync(this);

            _jumpSensor = new RunnerSensor
            {
                AreaCollider = jumpTriggerArea,
            };
            _jumpSensor.Sync(this);
        }

        private void Update()
        {
            _jumpMotion.Height = jumpPower;
            jumpPower = _jumpMotion.Height;

            _jumpSensor.AreaCollider = jumpTriggerArea;

            if (_groundSensor.IsGround() && _jumpSensor.IsRunnerStaying) _jumpMotion.Trigger();
        }
    }
}