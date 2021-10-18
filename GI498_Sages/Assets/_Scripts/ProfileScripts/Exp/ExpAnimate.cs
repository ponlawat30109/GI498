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
        //XPText.SetText(xp.ToString("N0"));
        XPText.text = xp.ToString("N0");
        Debug.Log("SetText");
    }


    public void EndAnimation()
    {
        Debug.Log("EndAnimation");
        RankManager.Instance.SliderUpdate();
        //Destroy(this);
    }


}
