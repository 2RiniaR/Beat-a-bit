using System;
using DG.Tweening;
using RineaR.BeatABit.Helpers;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Monsters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bird : MonoBehaviour
    {
        public float speed;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>() ?? throw new NullReferenceException();
        }

        private void Start()
        {
            _rigidbody2D.DOMove(_rigidbody2D.position + AngleHelper.AngleToVector2(_rigidbody2D.rotation) * speed, 1f)
                .SetEase(Ease.InOutQuint)
                .SetLoops(-1, LoopType.Incremental)
                .SetLink(gameObject);
        }
    }
}