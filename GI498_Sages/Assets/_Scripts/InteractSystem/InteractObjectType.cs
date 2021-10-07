using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjectType : MonoBehaviour
{
    public static InteractObjectType instance;
    public enum ObjectType
    {
        Item,
        Object
    };

    public ObjectType _objectType;
    public bool isClick = false;
    public bool isPlayerNearby = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnMouseDown()
    {
        if (isPlayerNearby)
        {
            isClick = true;
        }

        // isClick = isPlayerNearby ? true : false;
    }

    // private void OnMouseUp()
    // {
    //     if (isPlayerNearby)
    //     {
    //         isClick = false;
    //     }
    // }

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
