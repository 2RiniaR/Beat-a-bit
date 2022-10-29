using System;
using RineaR.BeatABit.Core;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.UI.StagePlaying
{
    [RequireComponent(typeof(Animator))]
    public class RunnerView : MonoBehaviour
    {
        public AthleticSystem system;
        public string lifeInteger = "Life";
        private Animator _animator;

        private void Awake()
        {
            system ??= GetComponentInParent<AthleticSystem>();
            _animator = GetComponent<Animator>() ?? throw new NullReferenceException();
        }

        private void Reset()
        {
            system = GetComponentInParent<AthleticSystem>();
        }

        private void Start()
        {
            system.OnReadyAsObservable().Subscribe(_ => OnSystemReady()).AddTo(this);
        }

        private void OnSystemReady()
        {
            system.runner.Life.OnChangedAsObservable().Subscribe(OnLifeChanged).AddTo(this);
        }

        private void OnLifeChanged(int life)
        {
            _animator.SetInteger(lifeInteger, life);
        }
    }
}