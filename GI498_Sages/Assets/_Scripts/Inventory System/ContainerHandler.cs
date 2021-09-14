using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Inventory_System;
using UnityEngine;

public class ContainerHandler : MonoBehaviour
{
    public ItemContainer inventory;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();

        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }

    }

    private void OnApplicationQuit()
    {
        // Save or somethings...
        inventory.Container.Clear();
    }
}
