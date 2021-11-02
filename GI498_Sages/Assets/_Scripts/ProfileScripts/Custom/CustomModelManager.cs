using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ModelScript;

public class CustomModelManager : MonoBehaviour
{
    [SerializeField] private Button ConfirmExitCustom;
    private CustomData customData;
    [SerializeField] private GameObject playerPref;


    [Header("Body Skin Color")]
    [SerializeField] private Button bodySkinMatSelectLeft;
    [SerializeField] private Button bodySkinMatSelectRight;

    [Header("Hair")]
    [SerializeField] private Button hairSelectLeft;
    [SerializeField] private Button hairSelectRight;

    [Header("Hair Color")]
    [SerializeField] private Button hairColorSelectLeft;
    [SerializeField] private Button hairColorSelectRight;

    [Header("Face")]
    [SerializeField] private Button faceSelectLeft;
    [SerializeField] private Button faceSelectRight;

    [Header("Outfit")]
    [SerializeField] private Button outfitSelectLeft;
    [SerializeField] private Button outfitSelectRight;

    [Header("Hat")]
    [SerializeField] private Button hatSelectLeft;
    [SerializeField] private Button hatSelectRight;

    [Header("Mouth")]
    [SerializeField] private Button mouthSelectLeft;
    [SerializeField] private Button mouthSelectRight;

    private ComponentSet[] componentSets;

    private void Start()
    {

        ConfirmExitCustom.onClick.AddListener(() => LoadCustomData());

        bodySkinMatSelectLeft.onClick.AddListener(() => SetComponentMat("Bodys", -1));
        bodySkinMatSelectRight.onClick.AddListener(() => SetComponentMat("Bodys", 1));

        hairSelectLeft.onClick.AddListener(() => SetComponentActive("Hairs", -1));
        hairSelectRight.onClick.AddListener(() => SetComponentActive("Hairs", 1));

        hairColorSelectLeft.onClick.AddListener(() => SetComponentMat("Hairs", -1));
        hairColorSelectRight.onClick.AddListener(() => SetComponentMat("Hairs", 1));

        faceSelectLeft.onClick.AddListener(() => SetComponentActive("Faces", -1));
        faceSelectRight.onClick.AddListener(() => SetComponentActive("Faces", 1));

        outfitSelectLeft.onClick.AddListener(() => SetComponentActive("Outfits", -1));
        outfitSelectRight.onClick.AddListener(() => SetComponentActive("Outfits", 1));

        hatSelectLeft.onClick.AddListener(() => SetComponentActive("Hats", -1));
        hatSelectRight.onClick.AddListener(() => SetComponentActive("Hats", 1));

        mouthSelectLeft.onClick.AddListener(() => SetComponentActive("Mouth", -1));
        mouthSelectRight.onClick.AddListener(() => SetComponentActive("Mouth", 1));

    }

    private void SetComponentActive(string setName, int selector)
    {
        ComponentSet components = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
        if (components == null)
        {
            Debug.Log("components null: setName_ " + setName);
            return;
        }

        if(components.canChangeObj == false)
        {
            Debug.Log("can't change: setName_ " + setName);
            return;
        }

        var objs = components.objs;
        int index = components.activeIndex += selector;

        if (objs[components.activeIndex].component != null)
            objs[components.activeIndex].component.SetActive(false);

        if (index < 0) index = objs.Length - 1;
        else if (index >= objs.Length) index = 0;

        if (objs[index].component != null)
            objs[index].component.SetActive(true);

        components.activeIndex = index;
    }

    public void SetComponentMat(string setName, int selector)
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
        var index = components.matIndex + selector;

        if (index < 0) index = mats.Length - 1;
        else if (index >= mats.Length) index = 0;

        SetMatMultiObj(components.objs, mats[index].mat);

        components.matIndex = index;
    }

    public void SetMatMultiObj(ComponentSet.Component[] objs, Material mat)
    {
        foreach (ComponentSet.Component obj in objs)
        {
            if (obj.component != null)
                obj.component.GetComponent<Renderer>().material = mat;
        }
    }

    #region SaveLoad

    public CustomData SaveCustomData()
    {
        customData = new CustomData();
        var datas = customData.datas;
        foreach (ComponentSet set in componentSets)
        {
            if (set.canChangeObj == true)
            {
                var data = new CustomData.Part();
                data.setName = set.setName;
                data.type = CustomData.IndexType.ActiveIndex;
                data.index = set.activeIndex;
                data.id = set.objs[data.index].id;
                datas.Add(data);
            }
            if (set.canChangeMat == true)
            {
                var data = new CustomData.Part();
                data.setName = set.setName;
                data.type = CustomData.IndexType.MatIndex;
                data.index = set.activeIndex;
                data.id = set.objs[data.index].id;
                datas.Add(data);
            }
        }

        if(_Scripts.SceneAnimator.Instance != null)
            _Scripts.SceneAnimator.Instance.customData = customData;

        return customData;
    }

    public void LoadCustomData(CustomData data)
    {
        if (data != null)
        {
            customData = data;
        }
        else
        {
            customData = new CustomData();
        }

        if(ModelComponent.Instance == null)
        {
            Instantiate(playerPref);
        }

        componentSets = ModelComponent.Instance.LoadData(customData);
        //ModelComponent.Instance.GetModel(out componentSets);
    }

    private void LoadCustomData()
    {
        ModelComponent.Instance.LoadData(customData);
    }

    #endregion
}
