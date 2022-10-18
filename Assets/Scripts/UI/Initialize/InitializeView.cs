using RineaR.BeatABit.Environments;
using UnityEngine;

namespace RineaR.BeatABit.UI.Initialize
{
    public class InitializeView : MonoBehaviour
    {
        private void Start()
        {
            SceneLoader.Current.ChangeScene(SceneType.Title, SceneType.Initialize);
        }
    }
}