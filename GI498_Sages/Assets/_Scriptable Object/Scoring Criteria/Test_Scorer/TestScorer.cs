using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScorer : MonoBehaviour
{
    public Scoring scorer;
    public LevelStandard levelStandard;

    [Header("1 Page")]
    public Text scoreText;
    public Text starText;

    [HideInInspector] public Nutrition nutr;
    [HideInInspector] public List<InputField> inputFields = new List<InputField>();

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

    private void Start()
    {
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
        CheckInputNull();

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
    }

    public void Calculate()
    {
        var resultScore = scorer.ValueCalculate(nutr, levelStandard);
        ShowResult(resultScore);
    }

    public void ShowResult(ResultScore resultScore)
    {
        scoreText.text = resultScore.finalScore.ToString();
        starText.text = resultScore.finalStar.ToString();
    }
}
