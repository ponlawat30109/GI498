using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientSlotInfoComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text detailText;

    public void Init(string title, string detail)
    {
        itemNameText.text = title;
        detailText.text = detail;
    }
}
