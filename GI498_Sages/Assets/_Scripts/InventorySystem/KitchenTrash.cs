using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class KitchenTrash : MonoBehaviour
{
    // private void OnTriggerStay(Collider other)
    // {
    //     // if (other.CompareTag("Player"))
    //     // {
    //     //     Debug.Log("Player");
    //     // }

    //     Destroy(other.gameObject);
    // }

    public _Scripts.InventorySystem.MiniStorage trashBin;

    private async void Update()
    {
        await Task.Delay(System.TimeSpan.FromSeconds(5));
        trashBin.ClearHolding();
        trashBin.ClearModel();

        // Debug.Log("Tashbin clear");

        // StartCoroutine(ClearBin());
    }

    // public IEnumerator ClearBin()
    // {
    //     yield return new WaitForSeconds(3);
    //     trashBin.ClearHolding();
    //     trashBin.ClearModel();
    // }
}
