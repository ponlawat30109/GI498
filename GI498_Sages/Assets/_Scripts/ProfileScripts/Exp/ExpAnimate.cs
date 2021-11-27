using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExpAnimate : MonoBehaviour
{
    public TextMeshProUGUI XPText;

    private void Awake()
    {
        XPText.text = string.Empty;
    }

    public void SetXP(int xp)
    {
        XPText.text = xp.ToString("N0");
    }

    public void SliderUpdate()
    {
        RankManager.Instance.SliderUpdate();
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }


}
