using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomModelManager : MonoBehaviour
{
    [SerializeField] private Button ConfirmExitCustom;
    private CustomData customData;
    [SerializeField] private GameObject playerPref;

    [SerializeField] private Material[] mats;

    [Header("Body Skin Color")]
    [SerializeField] private Button bodySkinMatSelectLeft;
    [SerializeField] private Button bodySkinMatSelectRight;
    private GameObject[] bodys;
    private int bodySkinMatIndex = 0;

    [Header("Hair")]
    [SerializeField] private Button hairSelectLeft;
    [SerializeField] private Button hairSelectRight;
    private GameObject[] hairs;
    private int hairIndex = 0;

    [Header("Hair Color")]
    [SerializeField] private Button hairColorSelectLeft;
    [SerializeField] private Button hairColorSelectRight;
    private int hairMatIndex = 0;

    [Header("Face")]
    [SerializeField] private Button faceSelectLeft;
    [SerializeField] private Button faceSelectRight;
    private GameObject[] faces;
    private int faceIndex = 0;

    [Header("Outfit")]
    [SerializeField] private Button outfitSelectLeft;
    [SerializeField] private Button outfitSelectRight;
    private GameObject[] outfits;
    private int outfitIndex = 0;

    [Header("Hat")]
    [SerializeField] private Button hatSelectLeft;
    [SerializeField] private Button hatSelectRight;
    private GameObject[] hats;
    private int hatIndex = 0;


    private void Start()
    {

        ConfirmExitCustom.onClick.AddListener(() => LoadCustomData());

        bodySkinMatSelectLeft.onClick.AddListener(() => SetBodySkinMat(-1));
        bodySkinMatSelectRight.onClick.AddListener(() => SetBodySkinMat(1));

        hairSelectLeft.onClick.AddListener(() => SetHairActive(-1));
        hairSelectRight.onClick.AddListener(() => SetHairActive(1));

        hairColorSelectLeft.onClick.AddListener(() => SetHairMat(-1));
        hairColorSelectRight.onClick.AddListener(() => SetHairMat(1));

        faceSelectLeft.onClick.AddListener(() => SetFaceActive(-1));
        faceSelectRight.onClick.AddListener(() => SetFaceActive(1));

        outfitSelectLeft.onClick.AddListener(() => SetOutfitActive(-1));
        outfitSelectRight.onClick.AddListener(() => SetOutfitActive(1));

        hatSelectLeft.onClick.AddListener(() => SetHatActive(-1));
        hatSelectRight.onClick.AddListener(() => SetHatActive(1));

    }

    private void SetMatMultiObj(GameObject[] objArray, Material mat)
    {
        foreach(GameObject obj in objArray)
        {
            if (!obj.name.Contains("Empty"))
                obj.GetComponent<Renderer>().material = mat;
        }
    }

    #region Set Mat or Set Active
    private void SetBodySkinMat(int selector)
    {
        if (bodys == null || mats == null)
        {
            Debug.Log("SetBodySkinMat Error: no body or no mat");
            return;
        }

        bodySkinMatIndex += selector;

        if (bodySkinMatIndex < 0) bodySkinMatIndex = mats.Length - 1;
        else if (bodySkinMatIndex >= mats.Length) bodySkinMatIndex = 0;

        SetMatMultiObj(bodys, mats[bodySkinMatIndex]);
    }
    private void DirectSetBodySkinMat(int selector)
    {
        if (bodys == null || mats == null)
        {
            if (bodys == null) Debug.Log("bodys == null");
            if (mats == null) Debug.Log("mats == null");
            Debug.Log("DirectSetBodySkinMat Error: no body or no mat");
            return;
        }
        else if(selector < 0 || selector >= mats.Length)
        {
            Debug.Log("DirectSetBodySkinMat Error: invalid mat index");
            return;
        }
        bodySkinMatIndex = selector;
        SetMatMultiObj(bodys, mats[bodySkinMatIndex]);
    }

    private void SetHairActive(int selector)
    {
        if (hairs == null)
        {
            Debug.Log("SetHairActive Error: no hair");
            return;
        }
        hairs[hairIndex].SetActive(false);
        hairIndex += selector;

        if (hairIndex < 0) hairIndex = hairs.Length - 1;
        else if (hairIndex >= hairs.Length) hairIndex = 0;

        hairs[hairIndex].SetActive(true);
    }

    private void DirectSetHairActive(int selector)
    {
        if (hairs == null)
        {
            Debug.Log("DirectSetHairActive Error: no hair");
            return;
        }
        else if (selector < 0 || selector >= hairs.Length)
        {
            Debug.Log("DirectSetHairActive Error: invalid mat index");
            return;
        }

        hairs[hairIndex].SetActive(false);
        hairIndex = selector;
        hairs[hairIndex].SetActive(true);
    }

    private void SetHairMat(int selector)
    {
        if (hairs == null || mats == null)
        {
            Debug.Log("SetHairMat Error: no hair or no mat");
            return;
        }

        hairMatIndex += selector;

        if (hairMatIndex < 0) hairMatIndex = mats.Length - 1;
        else if (hairMatIndex >= mats.Length) hairMatIndex = 0;

        SetMatMultiObj(hairs, mats[hairMatIndex]);
    }

    private void DirectSetHairMat(int selector)
    {
        if (hairs == null || mats == null)
        {
            Debug.Log("DirectSetHairMat Error: no hair or no mat");
            return;
        }
        else if (selector < 0 || selector >= mats.Length)
        {
            Debug.Log("DirectSetHairMat Error: invalid mat index");
            return;
        }
        hairMatIndex = selector;
        SetMatMultiObj(hairs, mats[hairMatIndex]);
    }

    private void SetFaceActive(int selector)
    {
        if (faces == null)
        {
            Debug.Log("SetFaceActive Error: no faces");
            return;
        }
        faces[faceIndex].SetActive(false);
        faceIndex += selector;

        if (faceIndex < 0) faceIndex = faces.Length - 1;
        else if (faceIndex >= faces.Length) faceIndex = 0;

        faces[faceIndex].SetActive(true);
    }

    private void DirectSetFaceActive(int selector)
    {
        if (faces == null)
        {
            Debug.Log("DirectSetFaceActive Error: no faces");
            return;
        }
        else if (selector < 0 || selector >= faces.Length)
        {
            Debug.Log("DirectSetHairActive Error: invalid mat index");
            return;
        }

        faces[faceIndex].SetActive(false);
        faceIndex = selector;
        faces[faceIndex].SetActive(true);
    }

    private void SetOutfitActive(int selector)
    {
        if (outfits == null)
        {
            Debug.Log("SetOutfitActive Error: no outfits");
            return;
        }
        outfits[outfitIndex].SetActive(false);
        outfitIndex += selector;

        if (outfitIndex < 0) outfitIndex = outfits.Length - 1;
        else if (outfitIndex >= outfits.Length) outfitIndex = 0;

        outfits[outfitIndex].SetActive(true);
    }

    private void DirectSetOutfitActive(int selector)
    {
        if (outfits == null)
        {
            Debug.Log("DirectSetOutfitActive Error: no outfits");
            return;
        }
        else if (selector < 0 || selector >= outfits.Length)
        {
            Debug.Log("DirectSetHairActive Error: invalid mat index");
            return;
        }

        outfits[outfitIndex].SetActive(false);
        outfitIndex = selector;
        outfits[outfitIndex].SetActive(true);
    }

    private void SetHatActive(int selector)
    {
        if (hats == null)
        {
            Debug.Log("SetHatActive Error: no hats");
            return;
        }
        hats[hatIndex].SetActive(false);
        hatIndex += selector;

        if (hatIndex < 0) hatIndex = hats.Length - 1;
        else if (hatIndex >= hats.Length) hatIndex = 0;

        hats[hatIndex].SetActive(true);

        Debug.Log("Hatindex : " + hatIndex);
    }

    private void DirectSetHatActive(int selector)
    {
        if (hats == null)
        {
            Debug.Log("DirectSetHatActive Error: no hats");
            return;
        }
        else if (selector < 0 || selector >= hats.Length)
        {
            Debug.Log($"DirectSetHairActive Error: invalid hat index: {selector}");
            return;
        }

        hats[hatIndex].SetActive(false);
        hatIndex = selector;
        hats[hatIndex].SetActive(true);
    }

    #endregion

    #region SaveLoad

    public CustomData SaveCustomData()
    {
        if(customData == null)
        {
            customData = new CustomData();
        }

        customData.bodySkin = bodySkinMatIndex;
        customData.hair = hairIndex;
        customData.hairColor = hairMatIndex;
        customData.face = faceIndex;
        customData.outfit = outfitIndex;
        customData.hat = hatIndex;

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
            customData.bodySkin = 0;
            customData.hair = 0;
            customData.hairColor = 0;
            customData.face = 0;
            customData.outfit = 0;
            customData.hat = 0;
        }

        if(ModelComponent.Instance == null)
        {
            Instantiate(playerPref);
        }
        ModelComponent.Instance.GetModel(out bodys, out hairs, out faces, out outfits, out hats);
        LoadCustomData();
    }

    private void LoadCustomData()
    {
        DirectSetBodySkinMat(customData.bodySkin);
        DirectSetHairActive(customData.hair);
        DirectSetHairMat(customData.hairColor);
        DirectSetFaceActive(customData.face);
        DirectSetOutfitActive(customData.outfit);
        DirectSetHatActive(customData.hat);
    }
    #endregion
}
