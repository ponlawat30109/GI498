using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomData
{
    public string modelVersion;
    public List<Part> datas;

    public CustomData()
    {
        modelVersion = ModelScript.ModelComponent.ModelVersion;
        datas = new List<Part>();
    }

    //public int bodySkin = 0;
    //public int hair = 0;
    //public int hairColor = 0;
    //public int face = 0;
    //public int outfit = 0;
    //public int hat = 0;
    //public int mouth = 0;

    //public Material bodySkin;
    //public GameObject hair;
    //public Material hairColor;
    //public GameObject face;
    //public GameObject outfit;
    //public GameObject hat;

    public class Part
    {
        public string setName;
        public IndexType type;
        public string id;
        public int index;
    }

    public enum IndexType
    {
        ActiveIndex,
        MatIndex
    }
}
