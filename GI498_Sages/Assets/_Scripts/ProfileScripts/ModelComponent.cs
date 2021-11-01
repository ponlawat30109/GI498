using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelScript
{
    public class ModelComponent : MonoBehaviour
    {
        [SerializeField] private ComponentSet[] componentSets;

        private static string modelVersion = "211101A1";
        public static string ModelVersion
        {
            get => modelVersion;
        }
        public static ModelComponent Instance;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }
            Instance = this;
        }

        public ComponentSet[] GetModel()
        {
            return componentSets;
        }
        //public void GetModel(out ComponentSet[] _componentSets)
        //{
        //    _componentSets = componentSets;
        //}

        public ComponentSet[] LoadData(CustomData customData)
        {
            if(customData.modelVersion != modelVersion)
            {
                //change by id
                foreach(CustomData.Part data in customData.datas)
                {
                    if(data.type == CustomData.IndexType.ActiveIndex)
                    {
                        DirectSetActive(data.setName, data.index);
                    }
                    else if(data.type == CustomData.IndexType.MatIndex)
                    {
                        DirectSetMat(data.setName, data.index);
                    }
                }
            }
            else
            {
                //change by index
                foreach(CustomData.Part data in customData.datas)
                {
                    if (data.type == CustomData.IndexType.ActiveIndex)
                    {
                        DirectSetActive(data.setName, data.id);
                    }
                    else if (data.type == CustomData.IndexType.MatIndex)
                    {
                        DirectSetMat(data.setName, data.id);
                    }
                }
            }
            return componentSets;
        }

        private void DirectSetActive(string setName, int index)
        {
            ComponentSet components = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
            if (components == null)
            {
                Debug.Log("components null: setName_ " + setName);
                return;
            }
            if (components.canChangeObj == false)
            {
                Debug.Log("can't change: setName_ " + setName);
                return;
            }

            var objs = components.objs;

            if (index < 0 || index >= objs.Length)
            {
                Debug.Log("invalid index: setName_ " + setName);
                return;
            }

            objs[components.activeIndex].SetActive(false);
            objs[index].SetActive(true);

            components.activeIndex = index;
        }

        private void DirectSetActive(string setName, string partId)
        {
            ComponentSet components = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
            if (components == null)
            {
                Debug.Log("components null: setName_ " + setName);
                return;
            }
            if (components.canChangeObj == false)
            {
                Debug.Log("can't change: setName_ " + setName);
                return;
            }

            var objs = components.objs;
            
            for(int i = 0; i < objs.Length; i++)
            {
                if (components.objIds[i] == partId)
                {
                    objs[components.activeIndex].SetActive(false);
                    objs[i].SetActive(true);
                    components.activeIndex = i;
                    break;
                }
            }
        }

        private void DirectSetMat(string setName, int index)
        {
            ComponentSet components = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
            if (components == null)
            {
                Debug.Log("components null: setName_ " + setName);
                return;
            }
            if (components.canChangeMat == false || components.mats == null)
            {
                Debug.Log("can't change: setName_ " + setName);
                return;
            }
            if (index < 0 || index >= components.mats.Length)
            {
                Debug.Log("invalid index: setName_ " + setName);
                return;
            }

            SetMatMultiObj(components.objs, components.mats[index]);
            components.matIndex = index;
        }

        private void DirectSetMat(string setName, string matId)
        {
            ComponentSet components = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
            if (components == null)
            {
                Debug.Log("components null: setName_ " + setName);
                return;
            }
            if (components.canChangeMat == false || components.mats == null)
            {
                Debug.Log("can't change: setName_ " + setName);
                return;
            }

            var mats = components.mats;

            for (int i = 0; i < mats.Length; i++)
            {
                if (components.matIds[i] == matId)
                {
                    SetMatMultiObj(components.objs, mats[i]);
                    components.matIndex = i;
                    break;
                }
            }
        }

        private void SetMatMultiObj(GameObject[] objs, Material mat)
        {
            foreach (GameObject obj in objs)
            {
                if (!obj.name.Contains("Empty"))
                    obj.GetComponent<Renderer>().material = mat;
            }
        }



        //private void Start()
        //{
        //    DontDestroyOnLoad(gameObject);
        //}

        //public void GetModel(out GameObject[] bodysArray, out GameObject[] hairsArray, out GameObject[] eyesArray, out GameObject[] outfitsArray, out GameObject[] hatsArray, out GameObject[] mouthsArray)
        //{
        //    bodysArray = bodys;
        //    hairsArray = hairs;
        //    eyesArray = eyes;
        //    outfitsArray = outfits;
        //    hatsArray = hats;
        //    mouthsArray = mouths;
        //}

    }
}