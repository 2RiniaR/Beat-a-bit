using RineaR.BeatABit.General;
using RineaR.BeatABit.Stages.Objects;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Motions
{
    public class BulletMotion
    {
        public Bullet Prefab { get; set; }
        public GameObject Actor { get; set; }
        public Transform EmitPoint { get; set; }

        public void Shoot()
        {
            var instance = Object.Instantiate(Prefab, EmitPoint.position, EmitPoint.rotation, Actor.transform.parent);
            instance.IgnoreTemporary(Actor.GetComponentInChildren<Collider2D>());

            var destroyTimer = instance.gameObject.AddComponent<DestroyTimer>();
            destroyTimer.duration = 5;

            var emitterReference = instance.gameObject.AddComponent<EmitterReference>();
            emitterReference.emitter = Actor;
        }
    }
}