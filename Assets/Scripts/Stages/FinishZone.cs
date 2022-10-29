using System;
using RineaR.BeatABit.Core;
using RineaR.BeatABit.Stages.Motions;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Stages
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(AthleticElement))]
    public class FinishZone : MonoBehaviour
    {
        public AttemptResult result;
        private AthleticElement _athleticElement;

        private RunnerSensor _runnerSensor;

        private void Awake()
        {
            _athleticElement = GetComponent<AthleticElement>() ?? throw new NullReferenceException();

            _runnerSensor = new RunnerSensor
            {
                AreaCollider = GetComponent<Collider2D>(),
            };
            _runnerSensor.Sync(this);
        }

        private void Start()
        {
            _runnerSensor.OnEnterAsObservable().Subscribe(_ => Trigger()).AddTo(this);
        }

        public void Trigger()
        {
            _athleticElement.System.Finish(result);
        }
    }
}