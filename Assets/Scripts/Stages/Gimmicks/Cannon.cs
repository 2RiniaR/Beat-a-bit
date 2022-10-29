using RineaR.BeatABit.Stages.Motions;
using RineaR.BeatABit.Stages.Objects;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Gimmicks
{
    public class Cannon : MonoBehaviour
    {
        public Bullet bulletPrefab;
        public Transform emitPoint;
        public float interval;
        private BulletMotion _bulletMotion;
        private TimeInterval _timeInterval;

        private void Awake()
        {
            _timeInterval = new TimeInterval
            {
                Interval = interval,
            };
            _timeInterval.Sync(this);

            _bulletMotion = new BulletMotion
            {
                Actor = gameObject,
                Prefab = bulletPrefab,
                EmitPoint = emitPoint,
            };
        }

        private void Start()
        {
            _timeInterval.OnTriggerAsObservable().Subscribe(_ => _bulletMotion.Shoot()).AddTo(this);
        }
    }
}