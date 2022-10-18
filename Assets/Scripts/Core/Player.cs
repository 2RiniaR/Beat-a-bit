using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace RineaR.BeatABit.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [Header("Current")]
        [Min(0)]
        public int life;

        public bool canMove = true;

        public bool fly;
        public bool hide;

        [Header("Settings")]
        [Min(0)]
        public float moveSpeed;

        [Min(0)]
        public float friction;

        [Min(0)]
        public float jumpPower;

        public UnityEvent onDeath = new();
        public UnityEvent<Goal> onGoal = new();

        public Collider2D holdZoneCollider;

        [CanBeNull]
        private Rigidbody2D _rigidbody2D;

        private int _wallLayerMask;

        private void Awake()
        {
            life = 3;
            _wallLayerMask = 1 << LayerMask.NameToLayer("Wall");
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>() ?? throw new NullReferenceException();
        }

        private void Update()
        {
            if (_rigidbody2D != null)
                _rigidbody2D.bodyType = canMove ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
        }

        private void FixedUpdate()
        {
            AddFrictionForce();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var goal = col.GetComponent<Goal>();
            if (goal == null) return;

            onGoal.Invoke(goal);
        }

        private void AddFrictionForce()
        {
            if (_rigidbody2D != null)
            {
                var force = new Vector2 { x = -_rigidbody2D.velocity.x * friction, y = 0 };
                _rigidbody2D.AddForce(force, ForceMode2D.Force);
            }
        }

        public void Move(Vector2 vector)
        {
            if (_rigidbody2D != null)
            {
                var force = new Vector2 { x = vector.x * moveSpeed, y = 0 };
                _rigidbody2D.AddForce(force, ForceMode2D.Force);
            }
        }

        public void Jump()
        {
            if (_rigidbody2D == null) return;

            var offset = new Vector2(0, -0.5f);
            var shape = new Vector2(0.95f, 0.01f);
            var hit = Physics2D.BoxCast(_rigidbody2D.position + offset, shape, 0, Vector2.down, 5000f, _wallLayerMask);

            if (fly || (hit.collider != null && hit.distance <= 0.01f))
                _rigidbody2D.velocity = new Vector2 { x = _rigidbody2D.velocity.x, y = jumpPower };
        }

        public void StartAct()
        {
        }

        public void FinishAct()
        {
        }
    }
}