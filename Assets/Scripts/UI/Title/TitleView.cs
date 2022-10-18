using RineaR.BeatABit.Environments;
using RineaR.BeatABit.General;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RineaR.BeatABit.UI.Title
{
    public class TitleView : MonoBehaviour
    {
        [Header("References")]
        public Button startButton;

        [Header("Properties")]
        public ProgressView transition;

        private void Start()
        {
            if (startButton)
            {
                EventSystem.current.SetSelectedGameObject(startButton.gameObject);
                startButton.OnClickAsObservable().Subscribe(_ => StartGame()).AddTo(this);
            }
        }

        public void StartGame()
        {
            SceneLoader.Current.ChangeScene(SceneType.StageSelect, SceneType.Title, transition);
        }
    }
}