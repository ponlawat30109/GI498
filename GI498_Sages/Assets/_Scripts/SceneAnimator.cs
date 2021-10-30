using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class SceneAnimator : MonoBehaviour
    {
        [SerializeField] private SceneHandler sceneHandler;
        [SerializeField] private bool loadStartManu = true;
        [SerializeField] private Canvas canvas;
        
        public string currentScene;
        
        [HideInInspector]
        public CustomData customData;
        public static SceneAnimator Instance;
        public bool onLoading;

        Animator loadingAnimation;
        List<AsyncOperation> sceneAsync = new List<AsyncOperation>();

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);

            Instance = this;
        }

        void Start()
        {
            if (loadStartManu == true)
            {
                LoadScene(SceneEnum.scn_MainMenu);
                loadStartManu = false;
            }
            loadingAnimation = GetComponent<Animator>();
            canvas.sortingOrder = -10;
        }

        public AsyncOperation LoadScene(string sceneName)
        {
            //Play Load Start Scene Animation
            currentScene = sceneName;
            return sceneHandler.LoadSpecificScene(currentScene);
        }

        public AsyncOperation LoadScene(SceneEnum sceneName)
        {
            return LoadScene(sceneName.ToString());
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

        public void ChangeScene(string unLoadScene,string loadScene)
        {
            //loadingAnimation.SetTrigger("Loading");
            //StopAllCoroutines();
            //StartCoroutine(WaitingforLoad(unloadScene, loadScene));
            string[] unloadScenes = { unLoadScene };
            string[] loadScenes = { loadScene };
            ChangeScene(unloadScenes, loadScenes);
        }

        public void ChangeScene(string[] unLoadScenes, string[] loadScenes)
        {
            loadingAnimation.SetTrigger("Loading");
            StopAllCoroutines();
            StartCoroutine(WaitingforLoad(unLoadScenes, loadScenes));
        }

        public void ChangeScene(SceneEnum unloadScene, SceneEnum loadScene)
        {
            ChangeScene(unloadScene.ToString(), loadScene.ToString());
        }

        public void ChangeScene(SceneEnum[] unloadScenes, SceneEnum[] loadScenes)
        {
            ChangeScene(unloadScenes.ToString(), loadScenes.ToString());
        }

        public void ChangeScene(string loadScene)
        {
            ChangeScene(currentScene, loadScene);
        }

        public void ChangeScene(SceneEnum loadScene)
        {
            ChangeScene(currentScene, loadScene.ToString());
        }

        private IEnumerator WaitingforLoad(string[] unloadScenes, string[] loadScenes)
        {
            canvas.sortingOrder = 10;
            while (onLoading == false)
            {
                yield return null;
            }

            //onLoading == true
            foreach (string scene in loadScenes)
            {
                sceneAsync.Add(LoadScene(scene));
            }
            
            
            foreach(string scene in unloadScenes)
            {
                UnLoadScene(scene);
            }

            while (sceneAsync.Count > 0)
            {
                for(int i = 0; i < sceneAsync.Count; i++)
                {
                    if (sceneAsync[i].progress >= 1)
                    {
                        sceneAsync.RemoveAt(i);
                        continue;
                    }
                }
                yield return null;
            }
            loadingAnimation.SetTrigger("Finish");

            while (onLoading == true)
            {
                yield return null;
            }
            canvas.sortingOrder = -10;
        }
    }

    public enum SceneEnum
    {
        scn_MainMenu,
        scn_Profile,
    }
}
