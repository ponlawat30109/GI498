using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Container", menuName = "Inventory System/Container")]
public class ItemContainer : ScriptableObject
{
    public List<ContainerSlot> Container = new List<ContainerSlot>();

    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;

        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            Container.Add(new ContainerSlot(_item, _amount));
        }
    }
    
}

[Serializable]
public class ContainerSlot
{
    public ItemObject item;
    public int amount;

    public ContainerSlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
        
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
