using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelScript
{
    public class ModelComponent : MonoBehaviour
    {
        [SerializeField] private ComponentSet[] componentSets;

        private static string modelVersion = "20211202A1";
        public static string ModelVersion
        {
            get => modelVersion;
        }

        private void Start()
        {
            var skinData = DataCarrier.customData;
            if (skinData != null)
                LoadData(skinData);
            //else
            //    return;
        }

        public ComponentSet[] GetModel()
        {
            return componentSets;
        }

        public ComponentSet[] LoadData(CustomData customData)
        {
            if (customData.datas != null && customData.datas.Count != 0)
            {
                if (customData.modelVersion != modelVersion)
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
            }
            return componentSets;
        }

        public void RandomSkin()
        {
            var rand = new System.Random();
            int index;
            foreach(ComponentSet set in componentSets)
            {
                if(set.canChangeObj == true)
                {
                    index = rand.Next(set.objs.Length);
                    SwitchActive(set, index);
                }
                if(set.canChangeMat == true)
                {
                    index = rand.Next(set.mats.Length);
                    SetMatMultiObj(set.objs, set.mats[index].mat);
                }
            }
        }

        public void ResetSkin()
        {
            foreach(ComponentSet set in componentSets)
            {
                if(set.canChangeObj == true)
                    SwitchActive(set, 0);
                if(set.canChangeMat == true)
                    SetMatMultiObj(set.objs, set.mats[0].mat);
            }
        }

        private void DirectSetActive(string setName, int index)
        {
            ComponentSet set = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
            if (set == null)
            {
                Debug.Log("components null: setName_ " + setName);
                return;
            }
            if (set.canChangeObj == false)
            {
                Debug.Log("can't change: setName_ " + setName);
                return;
            }

            if (index < 0 || index >= set.objs.Length)
            {
                Debug.Log("invalid index: setName_ " + setName);
                return;
            }
            SwitchActive(set, index);
        }

        private void DirectSetActive(string setName, string partId)
        {
            ComponentSet set = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
            if (set == null)
            {
                Debug.Log("components null: setName_ " + setName);
                return;
            }
            if (set.canChangeObj == false)
            {
                Debug.Log("can't change: setName_ " + setName);
                return;
            }

            var objs = set.objs;
            
            for(int i = 0; i < objs.Length; i++)
            {
                if (objs[i].id == partId)
                {
                    SwitchActive(set, i);
                    break;
                }
            }
        }

        private void SwitchActive(ComponentSet set, int toActiveIndex)
        {
            var objs = set.objs;
            if (objs[set.activeIndex].component != null)
                objs[set.activeIndex].component.SetActive(false);
            if (objs[toActiveIndex].component != null)
                objs[toActiveIndex].component.SetActive(true);
            set.activeIndex = toActiveIndex;
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
                if (obj.component != null)
                    obj.component.GetComponent<Renderer>().material = mat;
            }
        }

    }
}