using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarrier : MonoBehaviour
{
    public static DataCarrier Instance { get; private set; }

    private static PlayerData playerProfileData;
    public static PlayerData PlayerProfileData
    {
        get => playerProfileData;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public static void SetPlayerData(PlayerData playerData)
    {
        playerProfileData = playerData;
    }

    public static void SetComingExp(int xP)
    {
        playerProfileData.comingExp = xP;
    }
}
