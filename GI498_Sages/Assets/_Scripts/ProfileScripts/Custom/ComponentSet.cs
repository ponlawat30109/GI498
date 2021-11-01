using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelScript
{
    [System.Serializable]
    public class ComponentSet
    {
        public string setName;
        public Component[] objs;
        public int activeIndex;
        public bool canChangeObj;
        public bool canChangeMat;
        public Mat[] mats;
        public int matIndex;

        [System.Serializable]
        public class Component
        {
            public string name;
            public GameObject component;
            public string id;
        }

        [System.Serializable]
        public class Mat
        {
            public string name;
            public Material mat;
            public string id;
        }
    }
}
