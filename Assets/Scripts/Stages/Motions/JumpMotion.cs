using System;
using RineaR.BeatABit.General;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Motions
{
    /// <summary>
    ///     ジャンプモーション。
    /// </summary>
    public class JumpMotion : MonoBehaviourSync
    {
        private Step _currentStep = Step.None;

        private float _height;
        private float _triggeredY;

        public float Height
        {
            get => _height;
            set => _height = Mathf.Max(value, 0);
        }

        public Rigidbody2D Actor { get; set; }

        public void Trigger()
        {
            _triggeredY = Actor.position.y;
            _currentStep = Step.Initial;
        }

        public override void FixedUpdate()
        {
            switch (_currentStep)
            {
                case Step.None:
                    break;

                case Step.Initial:
                    Actor.velocity = new Vector2
                    {
                        x = Actor.velocity.x,
                        y = Mathf.Sqrt(2 * -Physics2D.gravity.y * Actor.gravityScale * Height),
                    };
                    _currentStep = Step.Free;
                    break;

                case Step.Free:
                    if (Actor.velocity.y <= 0.05)
                    {
                        _currentStep = Step.None;
                        return;
                    }

                    var targetY = _triggeredY + Height;
                    var diffY = targetY - Actor.position.y;
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

        private enum Step
        {
            None,
            Initial,
            Free,
        }
    }
}