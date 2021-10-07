using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public static RankManager Instance;

    [SerializeField] private Button chefRankButton;
    [SerializeField] private GameObject rankListPanel;
    [SerializeField] private GameObject rankBoxPrefab;
    [SerializeField] private Transform rankListViewportContent;
    [SerializeField] private Button chefRankCloseButton;
    [SerializeField] private Button blankAreaExit;

    [SerializeField] private List<Rank> rankList;

    //[SerializeField] private Button buttonRank;
    //[SerializeField] private Slider sliderEXP;
    //Player Profile
    //private int playerEXP;
    //private int gainEXP; //EXP from the lastest game

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        for (int i = 0; i < rankList.Count - 1; i++)
        {
            rankList[i].nextRank = rankList[i + 1];
        }
    }

    private void Start()
    {
        //Profile
        Debug.Assert(chefRankButton != null, "Profile UI: buttonChefRank is null");
        Debug.Assert(rankListPanel != null, "Profile UI: panelRankList is null");

        //Profile
        chefRankButton.onClick.AddListener(() => rankListPanel.SetActive(true));
        chefRankCloseButton.onClick.AddListener(() => rankListPanel.SetActive(false));
        blankAreaExit.onClick.AddListener(() => rankListPanel.SetActive(false));

        rankListPanel.SetActive(false);

        foreach(Rank r in rankList)
        {
            var rankbox = Instantiate(rankBoxPrefab, rankListViewportContent);
            rankbox.GetComponent<RankBox>().SetDetail(r);
        }

        //buttonRank.onClick.AddListener(() => gameObject.SetActive(true));
    }

    public Rank GetRank(int exp)
    {
        var rank = rankList[0];
        while (exp > rank.minExperience && rank.nextRank != null)
        {
            rank = rank.nextRank;
        }

        return rank;
    }
}
