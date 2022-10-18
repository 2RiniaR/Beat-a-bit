using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cysharp.Threading.Tasks;
using RineaR.BeatABit.General;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityScene = UnityEngine.SceneManagement.Scene;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace RineaR.BeatABit.Environments
{
    public class ApplicationStartup : SingletonMonoBehaviour<ApplicationStartup>
    {
        public SceneType firstScene = SceneType.Initialize;
        public bool reloadScenesOnPlay;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnAfterSceneLoad()
        {
            DebugManager.instance.enableRuntimeUI = false;

            InitializeSceneLoadingStates().Forget();
        }

        private static async UniTask InitializeSceneLoadingStates()
        {
            LoadCommonScene();

            await UniTask.Yield();

            var loadedScenes = GetLoadedScenes();
            if (Current.reloadScenesOnPlay) ReloadScenes(loadedScenes);
            LoadFirstSceneIfEmpty(loadedScenes);
        }

        private static void LoadCommonScene()
        {
            var commonScene =
                UnitySceneManager.GetSceneByName(SceneNameProvider.GetName(SceneType.Common));
            if (!commonScene.isLoaded)
            {
                UnitySceneManager.LoadScene(SceneNameProvider.GetName(SceneType.Common),
                    LoadSceneMode.Additive);
            }
        }

        private static ReadOnlyCollection<UnityScene> GetLoadedScenes()
        {
            var loadedScenes = new List<UnityScene>();
            for (var i = 0; i < UnitySceneManager.sceneCount; i++)
            {
                loadedScenes.Add(UnitySceneManager.GetSceneAt(i));
            }

            return loadedScenes.AsReadOnly();
        }

        private static void ReloadScenes(IEnumerable<UnityScene> targetScenes)
        {
            foreach (var targetScene in targetScenes)
            {
                var sceneType = SceneNameProvider.GetType(targetScene.name);
                if (sceneType is null or SceneType.Common) continue;
                SceneLoader.Current.ReloadScene(sceneType.Value);
            }
        }

        /// <summary>
        ///     Common 以外のシーンが1つも読み込まれていない場合、最初のシーンを読み込む。
        /// </summary>
        private static void LoadFirstSceneIfEmpty(IEnumerable<UnityScene> loadedScenes)
        {
            if (loadedScenes.Any(scene => scene.name != SceneNameProvider.GetName(SceneType.Common))) return;
            SceneLoader.Current.LoadScene(Current.firstScene);
        }
    }
}