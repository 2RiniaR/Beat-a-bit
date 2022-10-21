using System;
using UnityEngine;

namespace RineaR.BeatABit.Core
{
    [Serializable]
    public class GroundSensor
    {
        public LayerMask wallLayerMask;
        public Rigidbody2D Rigidbody2D { get; set; }

        public bool IsGround()
        {
            if (!Rigidbody2D) return false;

            var offset = new Vector2(0, -0.5f);
            var shape = new Vector2(0.95f, 0.01f);
            var hit = Physics2D.BoxCast(Rigidbody2D.position + offset, shape, 0, Vector2.down, 5000f, wallLayerMask);
            return hit.collider != null && hit.distance <= 0.01f;
        }
    }
}