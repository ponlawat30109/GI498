using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExpAnimate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankName;
    [SerializeField] private Image rankSprite;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI expText;
    private int countFPS = 30;
    private float Duration = 1f;
    private int remainExp = 0;
    private Rank currentRank;

    private Coroutine CountingCoroutine;

    public void UpdateExp(int gainExp)
    {
        if (CountingCoroutine != null)
        {
            SkipAnimation();
        }

        remainExp = gainExp;
        CountingCoroutine = StartCoroutine(CountExp(gainExp));
    }

    private void SkipAnimation()
    {
        expSlider.value += remainExp;
        while (currentRank.nextRank.minExperience < expSlider.value)
        {
            currentRank = currentRank.nextRank;
            PlayRankUpAnimation();
        }
        StopCoroutine(CountingCoroutine);
    }
    public void PlayRankUpAnimation()
    {
        rankName.text = currentRank.rankName;
        rankSprite.sprite = currentRank.sprite;
        expSlider.maxValue = currentRank.nextRank.minExperience;
        //Play Animation
    }

    private IEnumerator CountExp(int newExp)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / countFPS);
        int previousValue = Mathf.CeilToInt(expSlider.value);
        int stepAmount;
        if(newExp - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newExp - previousValue) / (countFPS * Duration));
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newExp - previousValue) / (countFPS * Duration));
        }

        if(previousValue < newExp)
        {
            while(previousValue < newExp)
            {
                previousValue += stepAmount;
                if(previousValue > newExp)
                {
                    previousValue = newExp;
                }
                expText.SetText(previousValue.ToString("N0"));

                yield return Wait;
            }
        }
        else
        {
            while (previousValue > newExp)
            {
                previousValue += stepAmount;
                if (previousValue < newExp)
                {
                    previousValue = newExp;
                }
                expText.SetText(previousValue.ToString("N0"));

                yield return Wait;
            }
        }
    }
}
