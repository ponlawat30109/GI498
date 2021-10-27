using System;
using System.Collections.Generic;
using UnityEditor;
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
            public SceneAsset sceneAsset;
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
                            SceneManager.LoadScene(scenePackage.sceneAsset.name, LoadSceneMode.Additive);
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
                        SceneManager.UnloadSceneAsync(scenePackage.sceneAsset.name);
                        scenePackage.isLoaded = false;
                    }
                }
            }
        }

        public void LoadSpecificScene()
        {
            if (sceneName != null || sceneName != "" || !sceneName.Contains(" "))
            {
                var index = GetScenePackageIndexByName(sceneName);
                var scene = scenePackages[index];
            
                if (scene.sceneAsset != null)
                {
                    SceneManager.LoadSceneAsync(scene.sceneAsset.name, LoadSceneMode.Additive);
                    scene.isLoaded = true;
                }
            }
        }

        public void LoadSpecificScene(string _sceneName)
        {
            sceneName = _sceneName;
            LoadSpecificScene();
        }

        public void UnloadSpecificScene()
        {
            if (sceneName != null || sceneName != "" || !sceneName.Contains(" "))
            {
                var index = GetScenePackageIndexByName(sceneName);
                var scene = scenePackages[index];
            
                if (scene.sceneAsset != null)
                {
                    SceneManager.UnloadSceneAsync(scene.sceneAsset.name);
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
            return scenePackages.FindIndex(result => result.sceneAsset.name == toGetSceneName);
        }
    }
}
