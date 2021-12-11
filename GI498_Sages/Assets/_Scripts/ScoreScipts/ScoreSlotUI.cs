using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlotUI : MonoBehaviour
{
    public Text contentTitle;
    public Text value;
    public Text unit;
    public List<GameObject> stars;

    public void ActiveStar(int number)
    {
        for(int i = 0; i<number; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
