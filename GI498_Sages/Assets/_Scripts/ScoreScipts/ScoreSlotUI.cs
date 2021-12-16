using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlotUI : MonoBehaviour
{
    public Sprite unactiveStar;
    public Sprite activeStar;
    public Text contentTitle;
    public Text value;
    public Text unit;
    public Text score;
    public Text detail;
    public List<Image> stars;

    public void ActiveStar(int number)
    {
        for(int i = 0; i< stars.Count; i++)
        {
            if (i < number)
            {
                stars[i].sprite = activeStar;
                stars[i].color = Color.white;
            }
            else
            {
                stars[i].sprite = unactiveStar;
                stars[i].color = Color.black;
            }
        }
    }
}
