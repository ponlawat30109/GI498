using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVChangeImage : MonoBehaviour
{
    public Image tvImage;
    public Sprite defaultSprite;

    private void Start()
    {
        SetDefaultImg();
    }

    public void TVChangeSprite(Sprite sprite)
    {
        tvImage.sprite = sprite;
    }

    public void SetDefaultImg()
    {
        tvImage.sprite = defaultSprite;
    }
}