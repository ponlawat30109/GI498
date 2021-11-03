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

    [System.Serializable]
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
