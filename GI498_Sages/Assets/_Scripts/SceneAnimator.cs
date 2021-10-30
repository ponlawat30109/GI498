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

        public void ChangeScene(string unloadScene,string loadScene)
        {
            //LoadingAnimation
            loadingAnimation.SetTrigger("Loading");
            StopAllCoroutines();
            StartCoroutine(countDownUnload(unloadScene, loadScene));
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

        private IEnumerator countDownUnload(string unloadScene, string loadScene)
        {
            canvas.sortingOrder = 10;
            while (onLoading == false)
            {
                yield return null;
            }

            //onLoading == true
            sceneAsync.Add(LoadScene(loadScene));
            UnLoadScene(unloadScene);

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
