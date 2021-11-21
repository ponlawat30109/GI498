using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVChangeImage : MonoBehaviour
{
    public static Image tvImage;

    public static void TVChangeSprite(Sprite sprite)
    {
        tvImage.sprite = sprite;
    }

    private void Start()
    {
        if(tvImage != null)
        {
            Destroy(this);
            return;
        }
        tvImage = GetComponent<Image>();
    }
}