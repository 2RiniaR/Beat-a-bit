using RineaR.BeatABit.Core;
using RineaR.BeatABit.UI.ChartEditing;
using UniRx;
using UnityEngine;

namespace RineaR.BeatABit.Environments.Scenes
{
    public class StagePlayScene : MonoBehaviour
    {
        public ChartEditor chartEditor;
        public AthleticSystem athleticSystem;

        private void Start()
        {
            if (chartEditor) chartEditor.chart = ApplicationSession.Current.stage.chart;
            if (athleticSystem) athleticSystem.athletic = ApplicationSession.Current.athletic;
            chartEditor.OnSubmittedAsObservable().Subscribe(OnChartEditFinished).AddTo(this);
        }

        private void OnChartEditFinished(ChartArrange arrange)
        {
            chartEditor.enabled = false;
            athleticSystem.chartPlayer.arrange = arrange;
            athleticSystem.enabled = true;
        }
    }
}