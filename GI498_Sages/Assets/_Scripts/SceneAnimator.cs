using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class SceneAnimator : MonoBehaviour
    {
        [SerializeField] private SceneHandler sceneHandler;
        [SerializeField] private bool loadStartManu = true;
        
        public string currentScene;
        
        [HideInInspector]
        public CustomData customData;
        public static SceneAnimator Instance;

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);

            Instance = this;
        }

        void Start()
        {
            sceneHandler = GetComponent<SceneHandler>();
            if(loadStartManu == true)
                LoadScene(SceneEnum.scn_MainMenu);
        }

        public void LoadScene(string sceneName)
        {
            //Play Load Start Scene Animation
            currentScene = sceneName;
            sceneHandler.LoadSpecificScene(currentScene);
        }

        public void LoadScene(SceneEnum sceneName)
        {
            LoadScene(sceneName.ToString());
        }

        public void UnLoadScene()
        {
            //UnloadAnimation
            sceneHandler.UnloadSpecificScene(currentScene);
        }

        public void UnLoadScene(string sceneName)
        {
            //UnloadAnimation
            sceneHandler.UnloadSpecificScene(sceneName);
        }

        public void UnLoadScene(SceneEnum sceneName)
        {
            UnLoadScene(sceneName.ToString());
        }

        public void ChangeScene(string unloadScene,string loadScene)
        {
            //UnloadAnimation
            UnLoadScene(unloadScene);
            LoadScene(loadScene);
            //Play Load Start Scene Animation
        }

        public void ChangeScene(SceneEnum unloadScene, SceneEnum loadScene)
        {
            ChangeScene(unloadScene.ToString(), loadScene.ToString());
        }

        public void ChangeScene(string loadScene)
        {
            ChangeScene(currentScene, loadScene);
        }

        public void ChangeScene(SceneEnum loadScene)
        {
            ChangeScene(currentScene, loadScene.ToString());
        }
    }

    public enum SceneEnum
    {
        scn_MainMenu,
        scn_Profile,
    }
}
