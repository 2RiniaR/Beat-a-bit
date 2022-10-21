using System;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    [Serializable]
    public class JumpMotion
    {
        public float startVelocity;
        public float finishVelocity;
        public float cancelVelocity;
        public float holdTime;

        private float _holdDuration;
        private bool _isMotionRunning;
        private float _simulationVelocity;

        public Rigidbody2D Rigidbody2D { get; set; }

        private void ValidateProperties()
        {
            startVelocity = Mathf.Max(startVelocity, 0);
            finishVelocity = Mathf.Max(finishVelocity, 0);
            cancelVelocity = Mathf.Max(cancelVelocity, 0);
            holdTime = Mathf.Max(holdTime, 0);
        }

        public void Trigger()
        {
            if (_isMotionRunning) return;

            ValidateProperties();
            _isMotionRunning = true;
            _holdDuration = 0;
            _simulationVelocity = startVelocity;
            Rigidbody2D.velocity = new Vector2 { x = Rigidbody2D.velocity.x, y = startVelocity };
        }

        public void UpdateVelocity()
        {
            if (!_isMotionRunning || !Rigidbody2D || Rigidbody2D.bodyType == RigidbodyType2D.Static) return;

            ValidateProperties();

            if (Rigidbody2D.velocity.y < cancelVelocity)
            {
                Cancel();
                return;
            }

            _holdDuration += Time.deltaTime;
            if (holdTime <= 0 || _holdDuration >= holdTime)
            {
                Finish();
                return;
            }

            var velocityReduction = (startVelocity - finishVelocity) * (Time.deltaTime / holdTime);
            _simulationVelocity -= velocityReduction;
            Rigidbody2D.velocity = new Vector2
            {
                x = Rigidbody2D.velocity.x,
                y = _simulationVelocity,
            };
        }

        public void Cancel()
        {
            if (!_isMotionRunning) return;

            _isMotionRunning = false;
            _holdDuration = 0;
        }

        public void Finish()
        {
            if (!_isMotionRunning) return;

            _isMotionRunning = false;
            _holdDuration = 0;
        }
    }
}