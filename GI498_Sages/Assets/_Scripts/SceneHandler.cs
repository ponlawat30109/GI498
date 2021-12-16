// #if UNITY_EDITOR

// using UnityEditor;

// #endif

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class SceneHandler : MonoBehaviour
    {
        [Serializable]
        struct ScenePackage
        {
            public bool needLoad;
            public bool isLoaded;
            public string sceneAsset;
        }

        [SerializeField] private List<ScenePackage> scenePackages;

        [Header("Single Scene Handler")]
        public string sceneName;

        public void LoadAllScene()
        {
            foreach (var scene in scenePackages)
            {
                if (scene.sceneAsset != null)
                {
                    var scenePackage = scene;
                    if (scenePackage.isLoaded == false)
                    {
                        if (scenePackage.needLoad)
                        {
                            SceneManager.LoadScene(scenePackage.sceneAsset, LoadSceneMode.Additive);
                            scenePackage.isLoaded = true;
                        }
                    }
                }
            }
        }

        public void UnloadAllScene()
        {
            foreach (var scene in scenePackages)
            {
                if (scene.sceneAsset != null)
                {
                    var scenePackage = scene;
                    if (scenePackage.isLoaded)
                    {
                        SceneManager.UnloadSceneAsync(scenePackage.sceneAsset);
                        scenePackage.isLoaded = false;
                    }
                }
            }
        }

        public AsyncOperation LoadSpecificScene()
        {
            if (sceneName != null || sceneName != "" || !sceneName.Contains(" "))
            {
                var index = GetScenePackageIndexByName(sceneName);
                var scene = scenePackages[index];

                if (scene.sceneAsset != null)
                {
                    var sceneAsync = SceneManager.LoadSceneAsync(scene.sceneAsset, LoadSceneMode.Additive);
                    scene.isLoaded = true;
                    return sceneAsync;
                }
            }

            return null;
        }

        public AsyncOperation LoadSpecificScene(string _sceneName)
        {
            sceneName = _sceneName;
            return LoadSpecificScene();
        }

        public void UnloadSpecificScene()
        {
            if (sceneName != null || sceneName != "" || !sceneName.Contains(" "))
            {
                var index = GetScenePackageIndexByName(sceneName);
                var scene = scenePackages[index];

                if (scene.sceneAsset != null)
                {
                    SceneManager.UnloadSceneAsync(scene.sceneAsset);
                    scene.isLoaded = false;
                }
            }
        }

        public void UnloadSpecificScene(string _sceneName)
        {
            sceneName = _sceneName;
            UnloadSpecificScene();
        }

        private int GetScenePackageIndexByName(string toGetSceneName)
        {
            return scenePackages.FindIndex(result => result.sceneAsset == toGetSceneName);
        }

        public void SetActiveScene(string scene)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
        }
    }

}
