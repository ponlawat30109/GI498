using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishScoreManager : MonoBehaviour
{
    private static DishScoreManager instance;
    public static DishScoreManager Instance { get => instance; }

    [SerializeField] private GameObject resultGroup;
    [SerializeField] private Scoring scorer;

    [SerializeField] private LevelStandard testLevelStandard;

    [SerializeField] private GameObject scoreSlotUIPrefab;

    [Header("1 Page")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text starText;
    [SerializeField] private GameObject stars;

    [Header("2 Page")]
    [SerializeField] private GameObject page2Group;
    [SerializeField] private List<ScoreSlotUI> fourthPriority_slotList;

    [Header("3 Page")]
    [SerializeField] private GameObject page3Group;
    [SerializeField] private Transform normalContentHolder;
    [SerializeField] private List<ScoreSlotUI> normalSlotList;

    [Header("4 Page")]
    [SerializeField] private GameObject page4Group;
    [SerializeField] private Transform problemContentHolder;
    [SerializeField] private List<ScoreSlotUI> problemSlotList;

    [HideInInspector] public Nutrition nutr;
    [HideInInspector] public List<InputField> inputFields = new List<InputField>();

    #region Nutrition
    [Header("Nutrition")]
    public InputField cholesterol;
    public InputField carbohydrate;
    public InputField sugars;
    public InputField fiber;
    public InputField proteins;
    public InputField fat;
    public InputField saturatedfat;
    public InputField water;
    public InputField potassium;
    public InputField sodium;
    public InputField calcium;
    public InputField phosphorus;
    public InputField magnesium;
    public InputField zinc;
    public InputField iron;
    public InputField manganese;
    public InputField copper;
    public InputField selenium;
    public InputField vitaminB1;
    public InputField vitaminB2;
    public InputField vitaminB3;
    public InputField vitaminB5;
    public InputField vitaminB6;
    public InputField vitaminB7;
    public InputField vitaminB9;
    public InputField vitaminB12;
    public InputField vitaminC;
    public InputField vitaminA;
    public InputField vitaminD;
    public InputField vitaminE;
    public InputField vitaminK;
    #endregion

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        OpenPage2();
        resultGroup.SetActive(false);

        //ForTest
        inputFields.Add(cholesterol);
        inputFields.Add(carbohydrate);
        inputFields.Add(sugars);
        inputFields.Add(fiber);
        inputFields.Add(proteins);
        inputFields.Add(fat);
        inputFields.Add(saturatedfat);
        inputFields.Add(water);
        inputFields.Add(potassium);
        inputFields.Add(sodium);
        inputFields.Add(calcium);
        inputFields.Add(phosphorus);
        inputFields.Add(magnesium);
        inputFields.Add(zinc);
        inputFields.Add(iron);
        inputFields.Add(manganese);
        inputFields.Add(copper);
        inputFields.Add(selenium);
        inputFields.Add(vitaminB1);
        inputFields.Add(vitaminB2);
        inputFields.Add(vitaminB3);
        inputFields.Add(vitaminB5);
        inputFields.Add(vitaminB6);
        inputFields.Add(vitaminB7);
        inputFields.Add(vitaminB9);
        inputFields.Add(vitaminB12);
        inputFields.Add(vitaminC);
        inputFields.Add(vitaminA);
        inputFields.Add(vitaminD);
        inputFields.Add(vitaminE);
        inputFields.Add(vitaminK);
    }
    
    public void AutoSetNutr()
    {
        if (cholesterol.text == string.Empty) cholesterol.text = "10000";
        if (carbohydrate.text == string.Empty) carbohydrate.text = "60000";
        if (sugars.text == string.Empty) sugars.text = "10000";
        if (fiber.text == string.Empty) fiber.text = "29000";
        if (proteins.text == string.Empty) proteins.text = "34000";
        if (fat.text == string.Empty) fat.text = "30000";
        if (saturatedfat.text == string.Empty) saturatedfat.text = "15000";
        if (water.text == string.Empty) water.text = "100000";
        if (potassium.text == string.Empty) potassium.text = "10000";
        if (sodium.text == string.Empty) sodium.text = "10000";
        if (calcium.text == string.Empty) calcium.text = "10000";
        if (phosphorus.text == string.Empty) phosphorus.text = "10000";
        if (magnesium.text == string.Empty) magnesium.text = "10000";
        if (zinc.text == string.Empty) zinc.text = "10000";
        if (iron.text == string.Empty) iron.text = "10000";
        if (manganese.text == string.Empty) manganese.text = "10000";
        if (copper.text == string.Empty) copper.text = "10000";
        if (selenium.text == string.Empty) selenium.text = "10000";
        if (vitaminB1.text == string.Empty) vitaminB1.text = "10000";
        if (vitaminB2.text == string.Empty) vitaminB2.text = "10000";
        if (vitaminB3.text == string.Empty) vitaminB3.text = "10000";
        if (vitaminB5.text == string.Empty) vitaminB5.text = "10000";
        if (vitaminB6.text == string.Empty) vitaminB6.text = "10000";
        if (vitaminB7.text == string.Empty) vitaminB7.text = "10000";
        if (vitaminB9.text == string.Empty) vitaminB9.text = "10000";
        if (vitaminB12.text == string.Empty) vitaminB12.text = "10000";
        if (vitaminC.text == string.Empty) vitaminC.text = "10000";
        if (vitaminA.text == string.Empty) vitaminA.text = "10000";
        if (vitaminD.text == string.Empty) vitaminD.text = "10000";
        if (vitaminE.text == string.Empty) vitaminE.text = "10000";
        if (vitaminK.text == string.Empty) vitaminK.text = "10000";
    }

    public void CheckInputNull()
    {
        foreach(var input in inputFields)
        {
            if(input.text == string.Empty)
            {
                input.text = "0";
            }
        }
    }

    public void SaveNutr()
    {
        nutr.cholesterol = float.Parse(cholesterol.text);
        nutr.carbohydrate = float.Parse(carbohydrate.text);
        nutr.sugars = float.Parse(sugars.text);
        nutr.fiber = float.Parse(fiber.text);
        nutr.proteins = float.Parse(proteins.text);
        nutr.fat = float.Parse(fat.text);
        nutr.saturatedfat = float.Parse(saturatedfat.text);
        nutr.water = float.Parse(water.text);
        nutr.potassium = float.Parse(potassium.text);
        nutr.sodium = float.Parse(sodium.text);
        nutr.calcium = float.Parse(calcium.text);
        nutr.phosphorus = float.Parse(phosphorus.text);
        nutr.magnesium = float.Parse(magnesium.text);
        nutr.zinc = float.Parse(zinc.text);
        nutr.iron = float.Parse(iron.text);
        nutr.manganese = float.Parse(manganese.text);
        nutr.copper = float.Parse(copper.text);
        nutr.selenium = float.Parse(selenium.text);
        nutr.vitaminB1 = float.Parse(vitaminB1.text);
        nutr.vitaminB2 = float.Parse(vitaminB2.text);
        nutr.vitaminB3 = float.Parse(vitaminB3.text);
        nutr.vitaminB5 = float.Parse(vitaminB5.text);
        nutr.vitaminB6 = float.Parse(vitaminB6.text);
        nutr.vitaminB7 = float.Parse(vitaminB7.text);
        nutr.vitaminB9 = float.Parse(vitaminB9.text);
        nutr.vitaminB12 = float.Parse(vitaminB12.text);
        nutr.vitaminC = float.Parse(vitaminC.text);
        nutr.vitaminA = float.Parse(vitaminA.text);
        nutr.vitaminD = float.Parse(vitaminD.text);
        nutr.vitaminE = float.Parse(vitaminE.text);
        nutr.vitaminK = float.Parse(vitaminK.text);

        CheckInputNull();
    }

    public void Calculate()
    {
        var resultScore = scorer.ValueCalculate(nutr, testLevelStandard);
        SetResult(resultScore);
    }

    public void Calculate(Nutrition nutr, LevelStandard levelStandard)
    {
        var resultScore = scorer.ValueCalculate(nutr, levelStandard);
        SetResult(resultScore);
        OpenPage2();
        resultGroup.SetActive(true);

        if (DataCarrier.Instance != null)
            DataCarrier.AddExp(resultScore.finalStar * 10);
    }

    public void Calculate(Nutrition nutr)
    {
        if(NPCScript.NPCManager.Instance != null)
        {
            if(NPCScript.NPCManager.Instance.LevelStandard != null)
            {
                var resultScore = scorer.ValueCalculate(nutr, NPCScript.NPCManager.Instance.LevelStandard);
                SetResult(resultScore);
                OpenPage2();
                resultGroup.SetActive(true);

                if (DataCarrier.Instance != null)
                    DataCarrier.AddExp(resultScore.finalStar * 10);
                    
                NPCScript.NPCManager.Instance.CompleteOrder();
                    
            }
            else
            {
                Debug.Log("Level Stander is null.");
            }
        }
        else
        {
            Debug.Log("NPCScript.NPCManager.Instance is null");
        }
        
    }

    public void SetResult(ResultScore resultScore)
    {
        scoreText.text = resultScore.finalScore.ToString("D");
        starText.text = resultScore.finalStar.ToString();

        int count_fourthPriority = 0;
        int count_normalContent = 0;
        int count_problemContent = 0;

        foreach (var scoreHolder in resultScore.allScore)
        {
            //Top4Priority
            if (scoreHolder.limiter.isTop4Priority == true)
            {
                if(count_fourthPriority < fourthPriority_slotList.Count)
                {
                    SetSlotDetail(fourthPriority_slotList[count_fourthPriority], scoreHolder);
                    count_fourthPriority++;
                }
            }

            //AllContent
            if (count_normalContent < normalSlotList.Count)
            {
                SetSlotDetail(normalSlotList[count_normalContent], scoreHolder);
            }
            else
            {
                normalSlotList.Add(CreateSlot(normalContentHolder, scoreHolder));
            }
            count_normalContent++;

            //ProblemContent
            if(scoreHolder.star < 3)
            {
                if (count_problemContent < problemSlotList.Count)
                {
                    SetSlotDetail(problemSlotList[count_problemContent], scoreHolder);
                }
                else
                {
                    problemSlotList.Add(CreateSlot(problemContentHolder, scoreHolder));
                }
                count_problemContent++;
            }
        }

        SetSlotActiveFalse(count_fourthPriority, fourthPriority_slotList);
        SetSlotActiveFalse(count_normalContent, normalSlotList);
        SetSlotActiveFalse(count_problemContent, problemSlotList);
    }

    public ScoreSlotUI CreateSlot(Transform parent, DishScoreHolder holder)
    {
        var slot = Instantiate(scoreSlotUIPrefab, parent);
        var ui = slot.GetComponent<ScoreSlotUI>();
        SetSlotDetail(ui, holder);
        return ui;
    }

    public void SetSlotDetail(ScoreSlotUI ui, DishScoreHolder holder)
    {
        ui.contentTitle.text = holder.limiter.name;
        ui.value.text = holder.value.ToString("0.##");
        ui.unit.text = holder.limiter.unitName;
        ui.score.text = holder.actualScore.ToString("D");
        ui.detail.text = holder.detail;
        ui.ActiveStar(holder.star);

        ui.gameObject.SetActive(true);
    }

    public void DestroySlot(List<ScoreSlotUI> slotList)
    {
        foreach(var ui in slotList)
        {
            Destroy(ui.gameObject);
        }
        slotList.Clear();
    }

    public void SetSlotActiveFalse(int index, List<ScoreSlotUI> slotList)
    {
        for(int i = index; i < slotList.Count; i++)
        {
            slotList[i].gameObject.SetActive(false);
        }
    }

    public void OpenPage2()
    {
        page2Group.SetActive(true);
        page3Group.SetActive(false);
        page4Group.SetActive(false);
    }
}
