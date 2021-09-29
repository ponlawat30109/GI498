using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    [Header("Inventory")]
    public ContainerObject inventory;
    public ContainerHandler inventoryHandler;
    [SerializeField] private int maxSlot = 1;
    [SerializeField] private bool isStackable;
    
    void Awake()
    {
        inventory.InitializeContainerObject(maxSlot, isStackable);
    }
}
