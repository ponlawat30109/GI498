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

    [Header("Hair")]
    [SerializeField] private Button hairSelectLeft;
    [SerializeField] private Button hairSelectRight;

    [Header("Hair Color")]
    [SerializeField] private Button hairColorSelectLeft;
    [SerializeField] private Button hairColorSelectRight;

    [Header("Eye")]
    [SerializeField] private Button eyeSelectLeft;
    [SerializeField] private Button eyeSelectRight;

    [Header("Mouth")]
    [SerializeField] private Button mouthSelectLeft;
    [SerializeField] private Button mouthSelectRight;

    [Header("Outfit")]
    [SerializeField] private Button outfitSelectLeft;
    [SerializeField] private Button outfitSelectRight;

    [Header("Hat")]
    [SerializeField] private Button hatSelectLeft;
    [SerializeField] private Button hatSelectRight;

    private ComponentSet[] componentSets;
    private GameObject playerObj;

    private void Start()
    {

        ConfirmExitCustom.onClick.AddListener(() => AutoSetCustomData());

        hairSelectLeft.onClick.AddListener(() => SetComponentActive("Hairs", -1));
        hairSelectRight.onClick.AddListener(() => SetComponentActive("Hairs", 1));

        hairColorSelectLeft.onClick.AddListener(() => SetComponentMat("Hairs", -1));
        hairColorSelectRight.onClick.AddListener(() => SetComponentMat("Hairs", 1));

        eyeSelectLeft.onClick.AddListener(() => SetComponentActive("Eyes", -1));
        eyeSelectRight.onClick.AddListener(() => SetComponentActive("Eyes", 1));

        mouthSelectLeft.onClick.AddListener(() => SetComponentActive("Mouths", -1));
        mouthSelectRight.onClick.AddListener(() => SetComponentActive("Mouths", 1));

        outfitSelectLeft.onClick.AddListener(() => SetComponentMat("Outfits", -1));
        outfitSelectRight.onClick.AddListener(() => SetComponentMat("Outfits", 1));

        hatSelectLeft.onClick.AddListener(() => SetComponentActive("Hats", -1));
        hatSelectRight.onClick.AddListener(() => SetComponentActive("Hats", 1));
    }

    #region CustomFunction
    private void SetComponentActive(string setName, int selector)
    {
        ComponentSet set = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
        if (set == null)
        {
            return;
        }

        if(set.canChangeObj == false)
        {
            return;
        }

        var objs = set.objs;
        int index = set.activeIndex + selector;
        if (objs[set.activeIndex].component != null)
            objs[set.activeIndex].component.SetActive(false);

        if (index < 0) index = objs.Length - 1;
        else if (index >= objs.Length) index = 0;

        if (objs[index].component != null)
            objs[index].component.SetActive(true);

        set.activeIndex = index;
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

    #endregion

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
                data.index = set.matIndex;
                data.id = set.mats[data.index].id;
                Debug.Log("data.index " + data.index);
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

        //if(ModelComponent.Instance == null)
        //{
        //    Instantiate(playerPref);
        //}

        //componentSets = ModelComponent.Instance.LoadData(customData);
        //ModelComponent.Instance.GetModel(out componentSets);

        if (componentSets == null)
        {
            playerObj = Instantiate(playerPref);
        }

        AutoSetCustomData();

    }

    private void AutoSetCustomData()
    {
        componentSets = playerObj.GetComponent<ModelComponent>().LoadData(customData);
    }

    private void OnDestroy()
    {
        Destroy(playerObj.gameObject);
    }

    #endregion
}
