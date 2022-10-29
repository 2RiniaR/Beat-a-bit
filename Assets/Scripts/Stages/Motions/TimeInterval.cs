using System;
using RineaR.BeatABit.General;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Motions
{
    public class TimeInterval : MonoBehaviourSync
    {
        private readonly Subject<int> _onTrigger = new();
        private int _countLimit = int.MaxValue;
        private float _interval = float.Epsilon;

        public float Interval
        {
            get => _interval;
            set => _interval = Mathf.Max(value, float.Epsilon);
        }

        public int CountLimit
        {
            get => _countLimit;
            set => _countLimit = Mathf.Max(value, 0);
        }

        public float Duration { get; private set; }
        public int TriggerCount { get; private set; }

        public override void Dispose()
        {
            _onTrigger.Dispose();
        }

        public IObservable<int> OnTriggerAsObservable()
        {
            return _onTrigger;
        }

        public override void Update()
        {
            if (CountLimit <= TriggerCount) return;

            Duration -= Time.deltaTime;
            if (Duration <= 0)
            {
                TriggerCount += 1;
                _onTrigger.OnNext(TriggerCount);
                Duration = Interval;
            }
        }
    }
}