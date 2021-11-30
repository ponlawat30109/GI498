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
        public static bool onLoading;

        Animator loadingAnimation;
        List<AsyncOperation> sceneAsync = new List<AsyncOperation>();
        List<string> strSceneToLoad = new List<string>();
        List<string> strSceneToUnLoad = new List<string>();


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
            currentScene = sceneName;
            return sceneHandler.LoadSpecificScene(currentScene);
        }

        public AsyncOperation LoadScene(SceneEnum sceneName)
        {
            return LoadScene(sceneName.ToString());
        }

        public void UnLoadScene()
        {
            sceneHandler.UnloadSpecificScene(currentScene);
        }

        public void UnLoadScene(string sceneName)
        {
            sceneHandler.UnloadSpecificScene(sceneName);
        }

        public void UnLoadScene(SceneEnum sceneName)
        {
            UnLoadScene(sceneName.ToString());
        }

        public void ChangeScene(string unloadScene,string loadScene)
        {
            strSceneToLoad.Add(loadScene);
            strSceneToUnLoad.Add(unloadScene);
            
            if (onLoading == false)
            {
                AnimateOnLoad();
            }
            else
                ManageLoadUnLoad();

        }

        public void ChangeScene(SceneEnum unloadScene, SceneEnum loadScene)
        {
            ChangeScene(unloadScene.ToString(), loadScene.ToString());
        }

        public void ChangeScene(string[] unloadScenes, string[] loadScenes)
        {
            foreach (string scene in loadScenes)
            {
                strSceneToLoad.Add(scene);
            }
            foreach (string scene in unloadScenes)
            {
                strSceneToUnLoad.Add(scene);
            }

            if (onLoading == false)
            {
                AnimateOnLoad();
            }
            else
                ManageLoadUnLoad();

        }

        public void ChangeScene(string loadScene)
        {
            ChangeScene(currentScene, loadScene);
        }

        public void ChangeScene(SceneEnum loadScene)
        {
            ChangeScene(currentScene, loadScene.ToString());
        }

        private IEnumerator countDownUnload()
        {
            ManageLoadUnLoad();
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
                Debug.Log("load scene count: " + sceneAsync.Count);
                yield return null;
            }
            loadingAnimation.SetTrigger("Finish");
        }

        private void ManageLoadUnLoad()
        {
            for (int i = 0; i < strSceneToUnLoad.Count; i++)
            {
                UnLoadScene(strSceneToUnLoad[i]);
            }
            strSceneToUnLoad.Clear();

            for (int i = 0; i < strSceneToLoad.Count; i++)
            {
                sceneAsync.Add(LoadScene(strSceneToLoad[i]));
            }
            strSceneToLoad.Clear();
        }

        public void StartCountDown()
        {
            StopAllCoroutines();
            StartCoroutine(countDownUnload());
        }

        public void AnimateOnLoad()
        {
            onLoading = true;
            canvas.sortingOrder = 10;
            loadingAnimation.SetTrigger("Loading");
        }

        public void AnimateOffLoad()
        {
            //this method run from animation.settrigger("Finish")
            onLoading = false;
            canvas.sortingOrder = -10;
        }
    }

    public enum SceneEnum
    {
        scn_MainMenu,
        scn_Profile,
    }
}
