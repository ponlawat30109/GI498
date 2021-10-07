using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Container", menuName = "Inventory System/Container")]
public class ContainerObject : ScriptableObject
{
    public List<ContainerSlot> Container = new List<ContainerSlot>();

    private int maxSlot;
    private bool isContainerStackable;


    public void InitializeContainerObject(int _maxSlot, bool isStackable)
    {
        maxSlot = _maxSlot;
        isContainerStackable = isStackable;
    }

    public bool AddItem(ItemObject itemToAdd)
    {
        bool successful = false;

        if (!HasFreeSpace())
        {
            return false;
        }

        if (isContainerStackable)
        {
            if (HasItem(itemToAdd))
            {
                var index = Container.FindIndex(x => x.item.Equals(itemToAdd));
                Container[index].AddAmount(1);
                successful = true;
            }
            else
            {
                if (HasFreeSpace())
                {
                    Container.Add(new ContainerSlot(itemToAdd, 1));
                    successful = true;
                }
            }
        }

        return successful;
    }

    public bool RemoveItem(ItemObject itemToRemove)
    {
        var successful = false;

        if (HasItem(itemToRemove))
        {
            var index = Container.FindIndex(x => x.item.Equals(itemToRemove));
            Container[index].SubAmount(1);
            successful = true;
        }
        else
        {
            Debug.Log($"Item {itemToRemove.itemName} can not remove (do not have item).");
        }

        return successful;
    }


    public bool HasItem(ItemObject itemToCheck)
    {
        var hasItem = false;

        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == itemToCheck) // If has item
            {
                hasItem = true;
                break;
            }
        }

        return hasItem;
    }

    public bool HasFreeSpace()
    {
        //Condition
        if (IsLimitedSlot())
        {
            if (GetSlotCount() < maxSlot)
            {
                return true; //Can add.
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public bool IsLimitedSlot()
    {
        if (maxSlot <= 0)
        {
            return true;
        }

        return false;
    }

    public int GetSlotCount()
    {
        return Container.Count;
    }

}

[Serializable]
public class ContainerSlot
{
    public ItemObject item;
    public int quantity;

    public ContainerSlot(ItemObject _item, int _quantity)
    {
        item = _item;
        quantity = _quantity;
    }

    public void AddAmount(int value)
    {
        quantity += value;
    }

    public void SubAmount(int value)
    {
        if (quantity - value <= 0)
        {
            quantity = 0; // Or Remove slot
            return;
        }
        quantity -= value;
    }
}
