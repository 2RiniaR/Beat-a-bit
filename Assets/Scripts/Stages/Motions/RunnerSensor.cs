using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RineaR.BeatABit.Core;
using RineaR.BeatABit.General;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace RineaR.BeatABit.Stages.Motions
{
    public class RunnerSensor : MonoBehaviourSync
    {
        private readonly List<Runner> _stayingRunners = new();
        public Collider2D AreaCollider { get; set; }
        public ReadOnlyCollection<Runner> StayingRunners => _stayingRunners.AsReadOnly();
        public bool IsRunnerStaying => StayingRunners.Count > 0;

        public override void OnEnable()
        {
            if (!AreaCollider) throw new InvalidOperationException();
            DisposeOnDisable(
                OnEnterAsObservable().Subscribe(runner => _stayingRunners.Add(runner)),
                OnExitAsObservable().Subscribe(runner => _stayingRunners.Remove(runner))
            );
        }

        public IObservable<Runner> OnEnterAsObservable()
        {
            if (!AreaCollider) throw new InvalidOperationException();
            return AreaCollider.OnTriggerEnter2DAsObservable()
                .Select(col => col.GetComponent<Runner>())
                .Where(runner => runner)
                .Publish()
                .RefCount();
        }

        public IObservable<Runner> OnExitAsObservable()
        {
            if (!AreaCollider) throw new InvalidOperationException();
            return AreaCollider.OnTriggerExit2DAsObservable()
                .Select(col => col.GetComponent<Runner>())
                .Where(runner => runner)
                .Publish()
                .RefCount();
        }
    }
}