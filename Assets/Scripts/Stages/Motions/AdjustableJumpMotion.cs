using System;
using RineaR.BeatABit.General;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Motions
{
    /// <summary>
    ///     高さが調整可能なジャンプモーション。
    /// </summary>
    public class AdjustableJumpMotion : MonoBehaviourSync
    {
        private Step _currentStep = Step.None;
        private float _currentTargetY;

        private float _maxHeight;
        private float _minHeight;
        private float _triggeredY;

        /// <summary>
        ///     シミュレーション中の速度。
        /// </summary>
        public float Velocity { get; private set; }

        /// <summary>
        ///     ジャンプを開始した瞬間の速度。
        /// </summary>
        public float MinHeight
        {
            get => _minHeight;
            set => _minHeight = Mathf.Max(value, 0);
        }

        /// <summary>
        ///     ジャンプを終了した瞬間の速度。
        /// </summary>
        public float MaxHeight
        {
            get => _maxHeight;
            set => _maxHeight = Mathf.Max(value, MinHeight);
        }

        public Rigidbody2D Actor { get; set; }

        public void Trigger()
        {
            _triggeredY = Actor.position.y;
            _currentStep = Step.Initial;
            _currentTargetY = _triggeredY + MinHeight;
        }

        public override void FixedUpdate()
        {
            UpdateVelocity();
        }

        private void UpdateVelocity()
        {
            switch (_currentStep)
            {
                case Step.None:
                    break;

                case Step.Initial:
                    Actor.velocity = new Vector2
                    {
                        x = Actor.velocity.x,
                        y = Mathf.Sqrt(2 * -Physics2D.gravity.y * Actor.gravityScale * MinHeight),
                    };
                    _currentStep = Step.Hold;
                    break;

                case Step.Hold:
                    if (Actor.velocity.y <= 0.05)
                    {
                        Cancel();
                        return;
                    }

                    _currentTargetY = Actor.position.y + MinHeight;
                    if (_currentTargetY >= _triggeredY + MaxHeight)
                    {
                        _currentTargetY = _triggeredY + MaxHeight;
                        _currentStep = Step.Free;
                    }

                    Actor.velocity = new Vector2
                    {
                        x = Actor.velocity.x,
                        y = Mathf.Sqrt(2 * -Physics2D.gravity.y * Actor.gravityScale * MinHeight),
                    };

                    break;

                case Step.Free:
                    if (Actor.velocity.y <= 0.05)
                    {
                        Cancel();
                        return;
                    }

                    var diffY = _currentTargetY - Actor.position.y;
                    Actor.velocity = new Vector2
                    {
                        x = Actor.velocity.x,
                        y = Mathf.Sqrt(2 * -Physics2D.gravity.y * Actor.gravityScale * diffY),
                    };
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Cancel()
        {
            _currentStep = Step.None;
        }

        public void Finish()
        {
            if (_currentStep == Step.None) return;
            _currentStep = Step.Free;
        }

        private enum Step
        {
            None,
            Initial,
            Hold,
            Free,
        }
    }
}