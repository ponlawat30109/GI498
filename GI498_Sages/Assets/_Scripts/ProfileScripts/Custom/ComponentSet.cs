using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelScript
{
    [System.Serializable]
    public class ComponentSet
    {
        public string setName;
        public GameObject[] objs;
        public string[] objIds;
        public int activeIndex;
        public bool canChangeObj;
        public bool canChangeMat;
        public Material[] mats;
        public string[] matIds;
        public int matIndex;
    }
}
