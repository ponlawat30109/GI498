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

    private static FoodObject order;
    public static FoodObject Order
    {
        get => order;
    }

    public static bool haveOrder;

    [SerializeField] private RankSystem playerRankHolder;
    private static List<FoodObject> foodList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        haveOrder = false;
        foodList = DataCarrier.Instance.playerRankHolder.FoodList;
    }

    public static void SetPlayerData(PlayerData playerData)
    {
        playerProfileData = playerData;
    }

    public static void SetComingExp(int xP)
    {
        playerProfileData.comingExp = xP;
    }

    public static FoodObject RandomFood()
    {
        var foodListRange = foodList.Count;
        var foodNumber = Random.Range(0, foodListRange);
        order = foodList[foodNumber];
        return order;
    }

    public static void CompleteOrder(int xp)
    {
        order = null;
        haveOrder = false;
        playerProfileData.comingExp += xp;
    }

    private void OnApplicationQuit()
    {
        playerRankHolder.ClearList();
    }
}
