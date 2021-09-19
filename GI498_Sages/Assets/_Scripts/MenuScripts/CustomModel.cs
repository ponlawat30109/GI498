using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomModel : MonoBehaviour
{
    [Header("Model Asset")]
    [SerializeField] private GameObject[] modelsToSelect;
    [Header("Selector")]
    [SerializeField] private Button selectorLeft;
    [SerializeField] private Button selectorRight;

    [Header("Material Asset")]
    [SerializeField] private Material[] materialToSelect;
    public int currentMaterialIndex;

    public int currentModelIndex;
    private GameObject currentModel;

    

    private void Start()
    {
        Debug.Assert(modelsToSelect != null, name + " : modelsToSelect is null");
        Debug.Assert(selectorLeft != null, name + " : selectorLeft is null");
        Debug.Assert(selectorRight != null, name + " : selectorRight is null");
        Debug.LogWarning("currentModel have to assign from load-save");
        currentModelIndex = 0;
        
        selectorLeft.onClick.AddListener(() => ChangeModel(-1));
        selectorRight.onClick.AddListener(() => ChangeModel(1));

        selectorLeft.onClick.AddListener(() => ChangeModelColor(-1));
        selectorLeft.onClick.AddListener(() => ChangeModelColor(1));
    }

    private void ChangeModel(int i)
    {
        currentModelIndex += i;
        
        if (currentModelIndex < 0)
            currentModelIndex = modelsToSelect.Length - 1;
        else if (currentModelIndex >= modelsToSelect.Length - 1)
            currentModelIndex = 0;

        currentModel.SetActive(false);

        currentModel = modelsToSelect[i];
        currentModel.SetActive(true);
    }

    private void ChangeModelColor(int i)
    {
        if (currentMaterialIndex < 0)
            currentMaterialIndex = materialToSelect.Length - 1;
        else if (currentMaterialIndex >= materialToSelect.Length - 1)
            currentMaterialIndex = 0;

        //เซ็ต Material ใหม่
        //currentModel.GetComponent<Material>(). = currentMaterial;
    }
}
