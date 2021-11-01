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

        public ComponentSet[] LoadData(CustomData customData)
        {
            if(customData.modelVersion != modelVersion)
            {
                //change by id
                foreach (CustomData.Part data in customData.datas)
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
            else
            {
                //change by index
                foreach (CustomData.Part data in customData.datas)
                {
                    if (data.type == CustomData.IndexType.ActiveIndex)
                    {
                        DirectSetActive(data.setName, data.index);
                    }
                    else if (data.type == CustomData.IndexType.MatIndex)
                    {
                        DirectSetMat(data.setName, data.index);
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

            if (!objs[components.activeIndex].name.Contains("Empty"))
                objs[components.activeIndex].component.SetActive(false);
            if (!objs[index].name.Contains("Empty"))
                objs[index].component.SetActive(true);

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
                if (objs[i].id == partId)
                {
                    if (!objs[components.activeIndex].name.Contains("Empty"))
                        objs[components.activeIndex].component.SetActive(false);
                    if (!objs[i].name.Contains("Empty"))
                        objs[i].component.SetActive(true);
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

            SetMatMultiObj(components.objs, components.mats[index].mat);
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
                if (mats[i].id == matId)
                {
                    SetMatMultiObj(components.objs, mats[i].mat);
                    components.matIndex = i;
                    break;
                }
            }
        }

        private void SetMatMultiObj(ComponentSet.Component[] objs, Material mat)
        {
            foreach (ComponentSet.Component obj in objs)
            {
                if (!obj.name.Contains("Empty"))
                    obj.component.GetComponent<Renderer>().material = mat;
            }
        }

    }
}