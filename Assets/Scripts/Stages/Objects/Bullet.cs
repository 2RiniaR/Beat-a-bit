using System;
using System.Collections.Generic;
using DG.Tweening;
using RineaR.BeatABit.Helpers;
using RineaR.BeatABit.Stages.Motions;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public bool throughWall;

        private readonly List<Collider2D> _temporaryIgnoreCollider = new();
        private Rigidbody2D _rigidbody2D;
        private TimeInterval _temporaryIgnoreTimer;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>() ?? throw new NullReferenceException();

            _temporaryIgnoreTimer = new TimeInterval
            {
                CountLimit = 1,
                Interval = 0.1f,
            };
            _temporaryIgnoreTimer.Sync(this);
        }

        private void Start()
        {
            _temporaryIgnoreTimer.OnTriggerAsObservable().Subscribe(_ => _temporaryIgnoreCollider.Clear()).AddTo(this);

            _rigidbody2D.DOMove(_rigidbody2D.position + AngleHelper.AngleToVector2(_rigidbody2D.rotation) * speed, 1f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental)
                .SetLink(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var index = _temporaryIgnoreCollider.FindIndex(x => x == other);
            if (index >= 0)
            {
                _temporaryIgnoreCollider.RemoveAt(index);
                return;
            }

            if (throughWall || other.isTrigger) return;

            Burst();
        }

        public void IgnoreTemporary(Collider2D ignoreCollider)
        {
            _temporaryIgnoreCollider.Add(ignoreCollider);
        }

        private void Burst()
        {
            Destroy(gameObject);
        }
    }
}