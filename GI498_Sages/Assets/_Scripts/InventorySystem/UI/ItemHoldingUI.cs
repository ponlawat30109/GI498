using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHoldingUI : MonoBehaviour
{
    [SerializeField] private ItemObject item;
    [SerializeField] private Image itemImage;
    
    [SerializeField] private GameObject parentContainer;

    public void InitializeItem(ItemObject toInitItem,GameObject _parentContainer)
    {
        item = toInitItem;
        itemImage.sprite = toInitItem.itemIcon;
        parentContainer = _parentContainer;
        transform.SetParent(parentContainer.transform);
    }
}
