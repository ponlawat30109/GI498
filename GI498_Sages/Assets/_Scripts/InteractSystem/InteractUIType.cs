using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool isPlayerNearby = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

    }

    private void OnMouseDown()
    {
        if (isPlayerNearby)
        {
            isClick = true;
        }

        // isClick = isPlayerNearby ? true : false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
