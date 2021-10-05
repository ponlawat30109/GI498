using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractUIType : MonoBehaviour
{
    public static InteractUIType instance;
    public enum UIType
    {
        Item,
        Object
    };

    public UIType _uitype;
    public bool isClick = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            isClick = true;
            Debug.Log("Test");
        }
    }
}
