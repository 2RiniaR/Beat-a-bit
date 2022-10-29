using RineaR.BeatABit.General;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Motions
{
    /// <summary>
    ///     地面を走るモーション。
    /// </summary>
    public class DashMotion : MonoBehaviourSync
    {
        private const float EpsilonVelocity = 0.01f;
        private float _accelerationTime;

        private float _decelerationTime;
        private float _input;
        private bool _isVelocitySignRight;
        private float _maxVelocityMagnitude;

        private float _velocityMagnitude;

        /// <summary>
        ///     最高速度。
        /// </summary>
        public float MaxVelocityMagnitude
        {
            get => _maxVelocityMagnitude;
            set => _maxVelocityMagnitude = Mathf.Max(value, 0f);
        }

        /// <summary>
        ///     静止状態から最高速度まで加速するまでの時間（秒）。
        /// </summary>
        public float AccelerationTime
        {
            get => _accelerationTime;
            set => _accelerationTime = Mathf.Max(value, EpsilonVelocity);
        }

        /// <summary>
        ///     最高速度から静止状態まで減速するまでの時間（秒）。
        /// </summary>
        public float DecelerationTime
        {
            get => _decelerationTime;
            set => _decelerationTime = Mathf.Max(value, EpsilonVelocity);
        }

        /// <summary>
        ///     現在の移動方向と移動量。
        /// </summary>
        public float Input
        {
            get => _input;
            set => _input = Mathf.Clamp(value, -1f, 1f);
        }

        public Rigidbody2D Actor { get; set; }

        public override void FixedUpdate()
        {
            UpdateVelocity();
        }

        public void UpdateVelocity()
        {
            if (!Actor) return;

            // 入力がニュートラルの場合
            if (Mathf.Abs(Input) <= EpsilonVelocity)
            {
                // 減速する
                var frameDeceleration = MaxVelocityMagnitude * Time.deltaTime / DecelerationTime;
                _velocityMagnitude -= frameDeceleration;
                _velocityMagnitude = Mathf.Clamp(_velocityMagnitude, 0f, MaxVelocityMagnitude);
            }
            // 入力がニュートラルでない場合
            else
            {
                var frameAcceleration = MaxVelocityMagnitude * Time.deltaTime / AccelerationTime;

                // 現在止まっている場合
                if (_velocityMagnitude <= EpsilonVelocity)
                {
                    _isVelocitySignRight = 0 <= Input;
                    _velocityMagnitude += frameAcceleration;
                    _velocityMagnitude = Mathf.Clamp(frameAcceleration, 0f, MaxVelocityMagnitude);
                }
                // 現在走っている方向が、入力方向と同じ場合
                else if ((_isVelocitySignRight && 0 <= Input) || (!_isVelocitySignRight && Input < 0))
                {
                    // 加速する
                    _velocityMagnitude += frameAcceleration;
                    _velocityMagnitude = Mathf.Clamp(_velocityMagnitude, 0f, MaxVelocityMagnitude);
                }
                // 現在走っている方向が、入力方向と逆の場合
                else
                {
                    // 速度を0にする
                    _velocityMagnitude = 0;
                }
            }

            Actor.velocity = new Vector2
            {
                x = (_isVelocitySignRight ? 1f : -1f) * _velocityMagnitude,
                y = Actor.velocity.y,
            };
        }
    }
}