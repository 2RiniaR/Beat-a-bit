using System.Collections.Generic;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Motions
{
    /// <summary>
    ///     着地しているかどうかを判定するセンサー。
    /// </summary>
    public class GroundSensor
    {
        /// <summary>
        ///     地面と判定するレイヤー。
        /// </summary>
        public LayerMask GroundLayerMask { get; set; } = LayerMask.GetMask("Block");

        public Rigidbody2D Actor { get; set; }

        public bool IsGround()
        {
            if (!Actor) return false;

            var offset = new Vector2(0, -0.5f);
            var shape = new Vector2(0.95f, 0.01f);

            var filter = new ContactFilter2D
            {
                layerMask = GroundLayerMask,
                useLayerMask = true,
            };
            var hits = new List<RaycastHit2D>();
            Physics2D.BoxCast(Actor.position + offset, shape, 0f, Vector2.down, filter, hits, 0.01f);
            return hits.Exists(hit => hit.collider &&
                                      !hit.collider.isTrigger &&
                                      !hit.transform.IsChildOf(Actor.transform) &&
                                      hit.distance <= 0.01f);
        }
    }
}