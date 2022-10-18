using RineaR.BeatABit.Core;
using RineaR.BeatABit.UI.ChartEditing;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Environments.Scenes
{
    public class StagePlaySystem : MonoBehaviour
    {
        public ChartEditor chartEditor;
        public ChartPlayer chartPlayer;
        public AttemptAthleticSystem attemptAthleticSystem;

        private readonly AsyncSubject<Unit> _onChartPlayerReady = new();

        private void Awake()
        {
            _onChartPlayerReady.AddTo(this);
        }

        private void Start()
        {
            InitializeComponents();

            _onChartPlayerReady.Subscribe(_ =>
            {
                chartEditor.enabled = false;
                attemptAthleticSystem.enabled = true;
            }).AddTo(this);
        }

        private void Update()
        {
            if (attemptAthleticSystem && attemptAthleticSystem.IsReady())
            {
                _onChartPlayerReady.OnNext(Unit.Default);
                _onChartPlayerReady.OnCompleted();
            }
        }

        private void InitializeComponents()
        {
            if (chartEditor)
            {
                chartEditor.chart = ApplicationSession.Current.stage.chart;
                chartEditor.Refresh();
            }

            var athletic = FindObjectOfType<Athletic>() ?? (ApplicationSession.Current.athletic
                ? Instantiate(ApplicationSession.Current.athletic, Vector3.zero, Quaternion.identity,
                    attemptAthleticSystem.transform)
                : null);
            if (athletic)
            {
                if (chartPlayer) chartPlayer.effectTarget = athletic.transform;
                if (attemptAthleticSystem) attemptAthleticSystem.athletic = athletic;
            }
        }
    }
}