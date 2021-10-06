using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomModelManager : MonoBehaviour
{
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
        if(CustomModel.Instance == null)
        {
            var player = Instantiate(playerPref);
            CustomModel.Instance = player.GetComponent<CustomModel>();
        }
        CustomModel.Instance.GetModel(out bodys, out hairs, out faces, out outfits, out hats);

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

    private void SetBodySkinMat(int selector)
    {
        if (bodys == null || mats == null)
        {
            Debug.Log("SetBodySkinMat Error: no body or no mat");
            return;
        }

        Debug.Log("BodyMat Before: " + bodySkinMatIndex);

        bodySkinMatIndex += selector;

        if (bodySkinMatIndex < 0) bodySkinMatIndex = mats.Length - 1;
        else if (bodySkinMatIndex >= mats.Length) bodySkinMatIndex = 0;

        foreach(GameObject obj in bodys)
        {
            obj.GetComponent<Renderer>().material = mats[bodySkinMatIndex];
        }
        Debug.Log("BodyMat After: " + bodySkinMatIndex);
    }

    private void SetHairActive(int selector)
    {
        if (hairs == null)
        {
            Debug.Log("SetBodySkinMat Error: no hair");
            return;
        }
        hairs[hairIndex].SetActive(false);
        hairIndex += selector;

        if (hairIndex < 0) hairIndex = hairs.Length - 1;
        else if (hairIndex >= hairs.Length) hairIndex = 0;

        hairs[hairIndex].SetActive(true);
    }

    private void SetHairMat(int selector)
    {
        if (hairs == null || mats == null)
        {
            Debug.Log("SetBodySkinMat Error: no hair or no mat");
            return;
        }

        hairMatIndex += selector;

        if (hairMatIndex < 0) hairMatIndex = mats.Length - 1;
        else if (hairMatIndex >= mats.Length) hairMatIndex = 0;

        foreach (GameObject obj in hairs)
        {
            if (!obj.name.Contains("Empty"))
                obj.GetComponent<Renderer>().material = mats[hairMatIndex];
        }
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

    private void SetHatActive(int selector)
    {
        if (hats == null)
        {
            Debug.Log("SetOutfitActive Error: no outfits");
            return;
        }
        hats[hatIndex].SetActive(false);
        hatIndex += selector;

        if (hatIndex < 0) hatIndex = hats.Length - 1;
        else if (hatIndex >= hats.Length) hatIndex = 0;

        hats[hatIndex].SetActive(true);
    }

    public CustomData SaveCustomData()
    {
        var customData = new CustomData()
        {
            bodySkin = bodySkinMatIndex,
            hair = hairIndex,
            hairColor = hairMatIndex,
            face = faceIndex,
            outfit = outfitIndex,
            hat = hatIndex

            //bodySkin = mats[bodySkinMatIndex],
            //hair = hairs[hairIndex],
            //hairColor = mats[hairMatIndex],
            //face = faces[faceIndex],
            //outfit = outfits[outfitIndex],
            //hat = hats[hatIndex]
        };
        return customData;
    }

    public void LoadCustomData(CustomData data)
    {
        bodySkinMatIndex = data.bodySkin;
        hairIndex = data.hair;
        hairMatIndex = data.hairColor;
        faceIndex = data.face;
        outfitIndex = data.outfit;
        hatIndex = data.outfit;
    }

    //public void GetPlayerModel(PlayerHolder playerHolder)
    //{
    //    CustomModel model = playerHolder.playerObj.GetComponent<CustomModel>();
    //    hairs = model.hairs;
    //    faces = model.faces;
    //    outfits = model.outfits;
    //    hats = model.hats;
    //}


}
