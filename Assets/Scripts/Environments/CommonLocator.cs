using RineaR.BeatABit.General;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RineaR.BeatABit.Environments
{
    public class CommonLocator : SingletonMonoBehaviour<CommonLocator>
    {
        public void Transfer(GameObject go)
        {
            SceneManager.MoveGameObjectToScene(go, gameObject.scene);
            go.transform.SetParent(transform);
        }
    }
}