using System;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    [Serializable]
    public class DashMotion
    {
        public float maxVelocity;
        public float accelerationTime;
        public float decelerationTime;
        public float input;
        public float epsilonVelocity = 0.01f;
        public Rigidbody2D Rigidbody2D { get; set; }

        private void ValidateProperties()
        {
            maxVelocity = Mathf.Max(maxVelocity, 0);
            accelerationTime = Mathf.Max(accelerationTime, float.Epsilon);
            decelerationTime = Mathf.Max(decelerationTime, float.Epsilon);
        }

        public void UpdateVelocity()
        {
            if (!Rigidbody2D || Rigidbody2D.bodyType == RigidbodyType2D.Static) return;

            ValidateProperties();

            var force = maxVelocity * input * Time.deltaTime / accelerationTime;
            var friction = Mathf.Abs(Rigidbody2D.velocity.x) > epsilonVelocity
                ? -Mathf.Sign(Rigidbody2D.velocity.x) * Time.deltaTime / decelerationTime
                : 0;
            Rigidbody2D.velocity = new Vector2
            {
                x = Mathf.Clamp(Rigidbody2D.velocity.x + friction + force, -maxVelocity, maxVelocity),
                y = Rigidbody2D.velocity.y,
            };
        }
    }
}