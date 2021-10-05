using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerInformationUI : MonoBehaviour
{
    [SerializeField] private TMP_Text infoNameText;
    [SerializeField] private TMP_Text infoDetailText;
    [SerializeField] private Image infoImage;

    public void InitializeInformation(string itemName,string detail, Sprite sprite)
    {
        infoNameText.text = itemName;
        infoDetailText.text = detail;
        infoImage.sprite = sprite;
    }

    public void Clear()
    {
        Destroy(this.gameObject);
    }
}
