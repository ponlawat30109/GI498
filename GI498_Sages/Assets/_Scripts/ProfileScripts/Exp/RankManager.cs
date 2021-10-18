using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankManager : MonoBehaviour
{
    public static RankManager Instance;

    [Header ("PlayerRank Visual")]
    [SerializeField] private Button chefRankButton;
    [SerializeField] private TextMeshProUGUI rankNameText;
    [SerializeField] private Image rankImage;
    [SerializeField] private Transform profilePanel;

    [Header("Slider Animation")]
    [SerializeField] private Slider expSlider;
    public int countFPS = 30;
    public float Duration = 1f;

    [Header("ExpAnimation")]
    public GameObject XPPrefab;

    [Header("RankList Visual")]
    [SerializeField] private GameObject rankListPanel;
    [SerializeField] private GameObject rankBoxPrefab;
    [SerializeField] private Transform rankListViewportContent;

    [Header ("Exit")]
    [SerializeField] private Button chefRankCloseButton;
    [SerializeField] private Button blankAreaExit;

    [Header("Player Rank Holder")]
    [SerializeField] private RankSystem playerRankHolder;
    //[SerializeField] private List<Rank> rankList;
    private List<RankBox> rankBoxList = new List<RankBox>();

    private int currentRankIndex = -1; //default for check
    private int gainExp = -1;
    private int currentExp = -1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        expSlider.maxValue = 0; //Default for check
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

        playerRankHolder.InitialHolder();
    }

    public void UpdateExp(int _currentExp, int _gainExp)
    {
        if(_currentExp == expSlider.value)
        {
            var xPAnimation = Instantiate(XPPrefab, profilePanel);
            xPAnimation.GetComponent<ExpAnimate>().SetXP(gainExp);
            currentExp = _currentExp;
            gainExp = _gainExp;
            SliderUpdate();
        }
        else
        {
            Debug.Log("RankManager, UpdateExp: currentExp != expSlider.value");
            Debug.Log("currentExp = "+ currentExp);
            Debug.Log("expSlider.value = " + expSlider.value);
        }
    }

    public void SliderUpdate()
    {
        StartCoroutine(SliderAnimation());
    }

    private IEnumerator SliderAnimation()
    {
        Debug.Log("Coroutine Slider Animation");
        var newExp = currentExp + gainExp;
        WaitForSeconds Wait = new WaitForSeconds(1f / countFPS);
        int previousValue = Mathf.CeilToInt(expSlider.value);
        int stepAmount;
        if (newExp - previousValue < 0)
        {
            yield break;
        }
        
        stepAmount = Mathf.CeilToInt((newExp - previousValue) / (countFPS * Duration));

        previousValue += stepAmount;
        if (previousValue > newExp)
        {
            previousValue = newExp;
        }

        //expText.SetText(previousValue.ToString("N0"));
        expSlider.value = previousValue;

        if(previousValue > expSlider.maxValue)
            UpRank();

        if(previousValue == newExp)
            yield break;

        yield return Wait;
    }

    public void InitialRankListPanel(int currentExp)
    {
        var rankList = playerRankHolder.RankList;
        for (int i = 0; i < rankList.Count; i++)
        {
            var targetRank = rankList[i];
            if (currentExp < targetRank.minExperience)
            {
                if (expSlider.maxValue == 0)
                {
                    currentRankIndex = i - 1;
                    SetCurrentRankVisual();
                }
                SetRankBox(targetRank, false);
            }
            else
            {
                if(targetRank.newRecipe)
                    playerRankHolder.AddFoodList(targetRank.newRecipe);
                if(targetRank.newIngredient)
                    playerRankHolder.AddIngredientList(targetRank.newIngredient);
                SetRankBox(targetRank, true);
            }
        }

        if (expSlider.maxValue == 0)
        {
            currentRankIndex = rankList.Count - 1;
            SetCurrentRankVisual();
        }
        expSlider.value = (float)currentExp;
    }

    private void UpRank()
    {
        SetCurrentRankVisual();
        //UpRankAnimation
        rankImage.GetComponent<Animator>().SetTrigger("RankUp");
    }

    public void SetCurrentRankVisual()
    {
        var rankList = playerRankHolder.RankList;
        var currentRank = rankList[currentRankIndex];
        rankNameText.SetText(currentRank.rankName);
        rankImage.sprite = currentRank.sprite;

        if (currentRankIndex + 1 < rankList.Count)
            expSlider.maxValue = rankList[currentRankIndex + 1].minExperience;
        else
            expSlider.maxValue = expSlider.value;
    }

    private void SetRankBox(Rank rank, bool isActive)
    {
        var rankBoxObj = Instantiate(rankBoxPrefab, rankListViewportContent);
        var rankBox = rankBoxObj.GetComponent<RankBox>();

        rankBox.SetDetail(rank, isActive);
        rankBoxList.Add(rankBox);
    }

    private void OnApplicationQuit()
    {
        playerRankHolder.ClearList();
    }
}
