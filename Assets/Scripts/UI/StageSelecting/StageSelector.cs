using RineaR.BeatABit.Core;
using RineaR.BeatABit.Environments;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RineaR.BeatABit.UI.StageSelecting
{
    public class StageSelector : MonoBehaviour
    {
        [Header("Properties")]
        public Stage stage;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            if (_button) _button.OnClickAsObservable().Subscribe(_ => Submit()).AddTo(this);
        }

        private void Update()
        {
            if (_button) _button.interactable = stage.unlockFlag.IsSet(ApplicationSession.Current);
        }

        public void Submit()
        {
            ApplicationSession.Current.stage = stage;
            SceneLoader.Current.ChangeScene(SceneType.StagePlay, SceneType.StageSelect);
        }
    }
}