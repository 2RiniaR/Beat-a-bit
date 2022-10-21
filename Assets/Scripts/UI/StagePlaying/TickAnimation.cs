using System.Collections.Generic;
using RineaR.BeatABit.Core;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RineaR.BeatABit.UI.StagePlaying
{
    public class TickAnimation : MonoBehaviour
    {
        public AthleticSystem system;
        public List<Graphic> badgeColored;
        public string tickTrigger = "Tick";
        private Animator _animator;

        private void Awake()
        {
            system ??= GetComponentInParent<AthleticSystem>();
            _animator = GetComponent<Animator>();
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
            system.chartPlayer.metronome.OnTickAsObservable().Subscribe(_ => OnTick()).AddTo(this);
            system.chartPlayer.ObserveEveryValueChanged(chartPlayer => chartPlayer.beat).Subscribe(OnBeatChanged)
                .AddTo(this);
        }

        private void OnTick()
        {
            if (_animator) _animator.SetTrigger(tickTrigger);
        }

        private void OnBeatChanged(int current)
        {
            var badge = system.chartPlayer.arrange.BadgeOf(current);
            var color = badge ? badge.color : Color.white;
            foreach (var graphic in badgeColored)
            {
                graphic.color = color;
            }
        }
    }
}