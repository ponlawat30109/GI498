using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInfoComponent : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;

    public void InitComponent(Sprite image, String name)
    {
        iconImage.sprite = image;
        nameText.text = name;
    }
}
