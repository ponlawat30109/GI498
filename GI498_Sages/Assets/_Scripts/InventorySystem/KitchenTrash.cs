using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class KitchenTrash : MonoBehaviour
{
    public _Scripts.InventorySystem.MiniStorage trashBin;

    private async void Update()
    {
        await Task.Delay(System.TimeSpan.FromSeconds(2));
        
        if (trashBin.IsCurrentItemNotNull())
        {
            trashBin.ClearHolding();
            trashBin.ClearModel();
        }
    }
}
