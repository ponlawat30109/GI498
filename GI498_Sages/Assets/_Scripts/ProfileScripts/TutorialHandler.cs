using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;

    void Start()
    {
        Debug.Log($"Profile\nName : {DataCarrier.PlayerProfileData.playerName}\nEXP : {DataCarrier.PlayerProfileData.exp}");

        if (DataCarrier.PlayerProfileData.exp > 0)
        {
            tutorialPanel.SetActive(false);
        }
    }
}
