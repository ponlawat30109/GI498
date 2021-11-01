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
        int index = components.activeIndex;

        objs[index].SetActive(false);

        index += selector;
        if (index < 0) index = objs.Length - 1;
        else if (index >= objs.Length) index = 0;

        objs[index].SetActive(true);

        components.activeIndex = index;
    }

    //private void DirectSetActive(string setName, int selector)
    //{
    //    ComponentSet components = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
    //    if (components == null)
    //    {
    //        Debug.Log("components null: setName_ " + setName);
    //        return;
    //    }
    //    if (components.canChangeObj == false)
    //    {
    //        Debug.Log("can't change: setName_ " + setName);
    //        return;
    //    }

    //    var objs = components.objs;

    //    if (selector < 0 || selector >= objs.Length)
    //    {
    //        Debug.Log("invalid index: setName_ " + setName);
    //        return;
    //    }

    //    objs[components.activeIndex].component.SetActive(false);
    //    objs[selector].component.SetActive(true);

    //    components.activeIndex = selector;
    //}

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

        SetMatMultiObj(components.objs, mats[index]);

        components.matIndex = index;
    }

    //public void DirectSetMat(string setName, int selector)
    //{
    //    ComponentSet components = Array.Find(componentSets, ComponentSet => ComponentSet.setName == setName);
    //    if (components == null)
    //    {
    //        Debug.Log("components null: setName_ " + setName);
    //        return;
    //    }
    //    if (components.canChangeMat == false || components.mats == null)
    //    {
    //        Debug.Log("can't change: setName_ " + setName);
    //        return;
    //    }
    //    if (selector < 0 || selector >= components.mats.Length)
    //    {
    //        Debug.Log("invalid index: setName_ " + setName);
    //        return;
    //    }

    //    SetMatMultiObj(components.objs, components.mats[selector].mat);
    //    components.matIndex = selector;
    //}

    public void SetMatMultiObj(GameObject[] objs, Material mat)
    {
        foreach (GameObject obj in objs)
        {
            if (!obj.name.Contains("Empty"))
                obj.GetComponent<Renderer>().material = mat;
        }
    }


    //private void SetMatMultiObj(GameObject[] objArray, Material mat)
    //{
    //    foreach(GameObject obj in objArray)
    //    {
    //        if (!obj.name.Contains("Empty"))
    //            obj.GetComponent<Renderer>().material = mat;
    //    }
    //}

    //#region Set Mat or Set Active
    //private void SetBodySkinMat(int selector)
    //{
    //    if (bodys == null || skinMats == null)
    //    {
    //        Debug.Log("SetBodySkinMat Error: no body or no mat");
    //        return;
    //    }

    //    bodySkinMatIndex += selector;

    //    if (bodySkinMatIndex < 0) bodySkinMatIndex = skinMats.Length - 1;
    //    else if (bodySkinMatIndex >= skinMats.Length) bodySkinMatIndex = 0;

    //    SetMatMultiObj(bodys, skinMats[bodySkinMatIndex]);
    //}
    //private void DirectSetBodySkinMat(int selector)
    //{
    //    if (bodys == null || skinMats == null)
    //    {
    //        if (bodys == null) Debug.Log("bodys == null");
    //        if (skinMats == null) Debug.Log("mats == null");
    //        Debug.Log("DirectSetBodySkinMat Error: no body or no mat");
    //        return;
    //    }
    //    else if(selector < 0 || selector >= skinMats.Length)
    //    {
    //        Debug.Log("DirectSetBodySkinMat Error: invalid mat index");
    //        return;
    //    }
    //    bodySkinMatIndex = selector;
    //    SetMatMultiObj(bodys, skinMats[bodySkinMatIndex]);
    //}

    //private void SetHairActive(int selector)
    //{
    //    if (hairs == null)
    //    {
    //        Debug.Log("SetHairActive Error: no hair");
    //        return;
    //    }
    //    hairs[hairIndex].SetActive(false);
    //    hairIndex += selector;

    //    if (hairIndex < 0) hairIndex = hairs.Length - 1;
    //    else if (hairIndex >= hairs.Length) hairIndex = 0;

    //    hairs[hairIndex].SetActive(true);
    //}

    //private void DirectSetHairActive(int selector)
    //{
    //    if (hairs == null)
    //    {
    //        Debug.Log("DirectSetHairActive Error: no hair");
    //        return;
    //    }
    //    else if (selector < 0 || selector >= hairs.Length)
    //    {
    //        Debug.Log("DirectSetHairActive Error: invalid mat index");
    //        return;
    //    }

    //    hairs[hairIndex].SetActive(false);
    //    hairIndex = selector;
    //    hairs[hairIndex].SetActive(true);
    //}

    //private void SetHairMat(int selector)
    //{
    //    if (hairs == null || skinMats == null)
    //    {
    //        Debug.Log("SetHairMat Error: no hair or no mat");
    //        return;
    //    }

    //    hairMatIndex += selector;

    //    if (hairMatIndex < 0) hairMatIndex = skinMats.Length - 1;
    //    else if (hairMatIndex >= skinMats.Length) hairMatIndex = 0;

    //    SetMatMultiObj(hairs, skinMats[hairMatIndex]);
    //}

    //private void DirectSetHairMat(int selector)
    //{
    //    if (hairs == null || skinMats == null)
    //    {
    //        Debug.Log("DirectSetHairMat Error: no hair or no mat");
    //        return;
    //    }
    //    else if (selector < 0 || selector >= skinMats.Length)
    //    {
    //        Debug.Log("DirectSetHairMat Error: invalid mat index");
    //        return;
    //    }
    //    hairMatIndex = selector;
    //    SetMatMultiObj(hairs, skinMats[hairMatIndex]);
    //}

    //private void SetFaceActive(int selector)
    //{
    //    if (faces == null)
    //    {
    //        Debug.Log("SetFaceActive Error: no faces");
    //        return;
    //    }
    //    faces[faceIndex].SetActive(false);
    //    faceIndex += selector;

    //    if (faceIndex < 0) faceIndex = faces.Length - 1;
    //    else if (faceIndex >= faces.Length) faceIndex = 0;

    //    faces[faceIndex].SetActive(true);
    //}

    //private void DirectSetFaceActive(int selector)
    //{
    //    if (faces == null)
    //    {
    //        Debug.Log("DirectSetFaceActive Error: no faces");
    //        return;
    //    }
    //    else if (selector < 0 || selector >= faces.Length)
    //    {
    //        Debug.Log("DirectSetHairActive Error: invalid mat index");
    //        return;
    //    }

    //    faces[faceIndex].SetActive(false);
    //    faceIndex = selector;
    //    faces[faceIndex].SetActive(true);
    //}

    //private void SetMouthActive(int selector)
    //{
    //    if (mouths == null)
    //    {
    //        Debug.Log("SetMouthActive Error: no mouths");
    //        return;
    //    }

    //    mouths[mouthIndex].SetActive(false);
    //    mouthIndex += selector;

    //    if (mouthIndex < 0) mouthIndex = mouths.Length - 1;
    //    else if (mouthIndex >= mouths.Length) mouthIndex = 0;

    //    mouths[mouthIndex].SetActive(true);
    //}

    //private void SetOutfitActive(int selector)
    //{
    //    if (outfits == null)
    //    {
    //        Debug.Log("SetOutfitActive Error: no outfits");
    //        return;
    //    }
    //    outfits[outfitIndex].SetActive(false);
    //    outfitIndex += selector;

    //    if (outfitIndex < 0) outfitIndex = outfits.Length - 1;
    //    else if (outfitIndex >= outfits.Length) outfitIndex = 0;

    //    outfits[outfitIndex].SetActive(true);
    //}

    //private void DirectSetOutfitActive(int selector)
    //{
    //    if (outfits == null)
    //    {
    //        Debug.Log("DirectSetOutfitActive Error: no outfits");
    //        return;
    //    }
    //    else if (selector < 0 || selector >= outfits.Length)
    //    {
    //        Debug.Log("DirectSetHairActive Error: invalid mat index");
    //        return;
    //    }

    //    outfits[outfitIndex].SetActive(false);
    //    outfitIndex = selector;
    //    outfits[outfitIndex].SetActive(true);
    //}

    //private void SetHatActive(int selector)
    //{
    //    if (hats == null)
    //    {
    //        Debug.Log("SetHatActive Error: no hats");
    //        return;
    //    }
    //    hats[hatIndex].SetActive(false);
    //    hatIndex += selector;

    //    if (hatIndex < 0) hatIndex = hats.Length - 1;
    //    else if (hatIndex >= hats.Length) hatIndex = 0;

    //    hats[hatIndex].SetActive(true);

    //    Debug.Log("Hatindex : " + hatIndex);
    //}

    //private void DirectSetHatActive(int selector)
    //{
    //    if (hats == null)
    //    {
    //        Debug.Log("DirectSetHatActive Error: no hats");
    //        return;
    //    }
    //    else if (selector < 0 || selector >= hats.Length)
    //    {
    //        Debug.Log($"DirectSetHairActive Error: invalid hat index: {selector}");
    //        return;
    //    }

    //    hats[hatIndex].SetActive(false);
    //    hatIndex = selector;
    //    hats[hatIndex].SetActive(true);
    //}

    //#endregion

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
                data.id = set.objIds[data.index];
                datas.Add(data);
            }
            if (set.canChangeMat == true)
            {
                var data = new CustomData.Part();
                data.setName = set.setName;
                data.type = CustomData.IndexType.MatIndex;
                data.index = set.activeIndex;
                data.id = set.matIds[data.index];
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
