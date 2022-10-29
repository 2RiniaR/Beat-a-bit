using System;
using RineaR.BeatABit.General;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Motions
{
    public class Life : MonoBehaviourSync
    {
        private readonly IntReactiveProperty _current = new(0);
        private readonly Subject<int> _onDamage = new();

        private float _invincibleDurationLeft;
        private float _invincibleDurationOnDamage;
        private int _max;

        public int Max
        {
            get => _max;
            set => _max = Mathf.Max(value, 0);
        }

        public int Current
        {
            get => _current.Value;
            private set => _current.Value = value;
        }

        public float InvincibleDurationOnDamage
        {
            get => _invincibleDurationOnDamage;
            set => _invincibleDurationOnDamage = Mathf.Max(value, 0);
        }

        public void Initialize()
        {
            Current = Max;
        }

        public IObservable<Unit> OnDeadAsObservable()
        {
            return _current.Where(x => x == 0).AsUnitObservable().Publish().RefCount();
        }

        public IObservable<int> OnChangedAsObservable()
        {
            return _current;
        }

        public IObservable<int> OnDamageAsObservable()
        {
            return _onDamage;
        }

        public void Damage(int strength)
        {
            Current = Mathf.Max(Current - strength, 0);
            _invincibleDurationLeft = InvincibleDurationOnDamage;
            _onDamage.OnNext(strength);
        }

        public override void Update()
        {
            _invincibleDurationLeft = Mathf.Max(_invincibleDurationLeft - Time.deltaTime, 0);
        }

        public override void Dispose()
        {
            _onDamage.Dispose();
        }
    }
}