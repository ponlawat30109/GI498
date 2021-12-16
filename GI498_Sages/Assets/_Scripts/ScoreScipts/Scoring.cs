using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scorer", menuName = "Scoring Criteria/Scorer")]
public class Scoring : ScriptableObject
{
    [Header("% score Level")]
    public float star5 = 100f;
    public float star4 = 75f;
    public float star3 = 50f;
    public float star2 = 25f;
    public float star1 = 0f;

    [SerializeField] private LevelStandard defaultStandard;
    private System.Random rnd = new System.Random();

    public ResultScore ValueCalculate(List<IngredientObject> ingredients, LevelStandard standard)
    {
        if(standard == null)
        {
            standard = defaultStandard;
        }
        var dishNutr = new Nutrition();
        SetZeroNutr(dishNutr);
        SumNitr(dishNutr, ingredients);
        return ValueCalculate(dishNutr, standard);

        //CalculateEachEnergy(carb, protein, etc) => save in DishScoreHolder
        //TotalEnergy = all ENergy => save in DishScoreHolder
        //Calculate other nutrition
        //var BaseTotalEnergy = CalculateScore(dishNutr, starndard.energrScore.totalEnergyLimit, dishScoreHolders);
    }

    public ResultScore ValueCalculate(Nutrition dishNutr, LevelStandard standard)
    {
        if (standard == null)
        {
            standard = defaultStandard;
        }
        ResultScore resultScore = new ResultScore();
        List<DishScoreHolder> allScore = new List<DishScoreHolder>();

        //default unit = milligram
        var changeUnit = 0.001f; // 1 milligram = 0.001 gram

        // 1 kCal = 4.18 kJ
        // 1 kJ = 0.24 kCal

        //var carbohydrateEnergy = (dishNutr.carbohydrate + dishNutr.sugars + dishNutr.fiber) * 4 * changeUnit;   // carb 1 g = 4 kCal = 17 kJ
        var carbohydrateEnergy = dishNutr.carbohydrate * 4 * changeUnit;
        var proteinEnergy = dishNutr.proteins * 4 * changeUnit;        // prot 1 g =  4 kCal = 17 kJ
        var fatEnergy = dishNutr.fat * 9 * changeUnit;   // fat 1 g = 9 kCal = 38 kJ // fat is already include saturatedfat fat
        var alcoholEnergy = 0f;        // alc 1 g = 7 kCal = 29 kJ
        alcoholEnergy *= 5.6f * changeUnit;         // alc 1 mL = 5.6 kCal = 23 kJ        

        var totalEnergy = carbohydrateEnergy + proteinEnergy + fatEnergy + alcoholEnergy;
        
        if (standard.limiterSet.totalEnergyLimit.weight > 0)
        {
            DishScoreHolder totalEnergyScore = new DishScoreHolder();
            if (standard.limiterSet.totalEnergyLimit.isTop4Priority == true)
                totalEnergyScore.isPriority = true;
            if (standard.limiterSet.totalEnergyLimit.limterType != LimiterType.None && standard.limiterSet.totalEnergyLimit.calType != CalculateType.None)
                totalEnergyScore.limiter = standard.limiterSet.totalEnergyLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.totalEnergyLimit.limterType != LimiterType.None && defaultStandard.limiterSet.totalEnergyLimit.calType != CalculateType.None)
                {
                    totalEnergyScore.limiter = defaultStandard.limiterSet.totalEnergyLimit;
                }
            }
            if (totalEnergyScore.limiter != null)
            {
                totalEnergyScore.value = totalEnergy;
                allScore.Add(totalEnergyScore);
            }
        }

        if (standard.limiterSet.carbLimit.weight > 0)
        {
            DishScoreHolder carbEnergyScore = new DishScoreHolder();
            if (standard.limiterSet.carbLimit.isTop4Priority == true)
                carbEnergyScore.isPriority = true;
            if (standard.limiterSet.carbLimit.limterType != LimiterType.None && standard.limiterSet.carbLimit.calType != CalculateType.None)
                carbEnergyScore.limiter = standard.limiterSet.carbLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.carbLimit.limterType != LimiterType.None && defaultStandard.limiterSet.carbLimit.calType != CalculateType.None)
                {
                    carbEnergyScore.limiter = defaultStandard.limiterSet.carbLimit;
                }
            }
            if (carbEnergyScore.limiter != null)
            {
                if (carbEnergyScore.limiter.calType == CalculateType.PercentEnergy)
                    carbEnergyScore.value = carbohydrateEnergy;
                else
                    carbEnergyScore.value = dishNutr.carbohydrate;
                allScore.Add(carbEnergyScore);
            }
        }

        if (standard.limiterSet.proteinLimit.weight > 0)
        {
            DishScoreHolder protEnergyScore = new DishScoreHolder();
            if (standard.limiterSet.proteinLimit.isTop4Priority == true)
                protEnergyScore.isPriority = true;
            if (standard.limiterSet.proteinLimit.limterType != LimiterType.None && standard.limiterSet.proteinLimit.calType != CalculateType.None)
                protEnergyScore.limiter = standard.limiterSet.proteinLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.proteinLimit.limterType != LimiterType.None && defaultStandard.limiterSet.proteinLimit.calType != CalculateType.None)
                {
                    protEnergyScore.limiter = defaultStandard.limiterSet.proteinLimit;
                }
            }
            if (protEnergyScore.limiter != null)
            {
                if (protEnergyScore.limiter.calType == CalculateType.PercentEnergy)
                    protEnergyScore.value = proteinEnergy;
                else
                    protEnergyScore.value = dishNutr.proteins;
                allScore.Add(protEnergyScore);
            }
        }

        if (standard.limiterSet.fatLimit.weight > 0)
        {
            DishScoreHolder fatEnergyScore = new DishScoreHolder();
            if (standard.limiterSet.fatLimit.isTop4Priority == true)
                fatEnergyScore.isPriority = true;
            if (standard.limiterSet.fatLimit.limterType != LimiterType.None && standard.limiterSet.fatLimit.calType != CalculateType.None)
                fatEnergyScore.limiter = standard.limiterSet.fatLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.fatLimit.limterType != LimiterType.None && defaultStandard.limiterSet.fatLimit.calType != CalculateType.None)
                {
                    fatEnergyScore.limiter = defaultStandard.limiterSet.fatLimit;
                }
            }
            if (fatEnergyScore.limiter != null)
            {
                if (fatEnergyScore.limiter.calType == CalculateType.PercentEnergy)
                    fatEnergyScore.value = fatEnergy;
                else
                    fatEnergyScore.value = dishNutr.fat;
                allScore.Add(fatEnergyScore);
            }
        }

        if (standard.limiterSet.saturatedFatLimit.weight > 0)
        {
            DishScoreHolder satFatScore = new DishScoreHolder();
            if (standard.limiterSet.saturatedFatLimit.isTop4Priority == true)
                satFatScore.isPriority = true;
            if (standard.limiterSet.saturatedFatLimit.limterType != LimiterType.None && standard.limiterSet.saturatedFatLimit.calType != CalculateType.None)
                satFatScore.limiter = standard.limiterSet.saturatedFatLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.saturatedFatLimit.limterType != LimiterType.None && defaultStandard.limiterSet.saturatedFatLimit.calType != CalculateType.None)
                {
                    satFatScore.limiter = defaultStandard.limiterSet.saturatedFatLimit;
                }
            }
            if (satFatScore.limiter != null)
            {
                if(satFatScore.limiter.calType == CalculateType.PercentEnergy)
                    satFatScore.value = dishNutr.saturatedfat * 9 * changeUnit;
                else
                    satFatScore.value = dishNutr.saturatedfat;
                allScore.Add(satFatScore);
            }
        }

        if (standard.limiterSet.sugarLimit.weight > 0)
        {
            DishScoreHolder sugarScore = new DishScoreHolder();
            if (standard.limiterSet.sugarLimit.isTop4Priority == true)
                sugarScore.isPriority = true;
            if (standard.limiterSet.sugarLimit.limterType != LimiterType.None && standard.limiterSet.sugarLimit.calType != CalculateType.None)
                sugarScore.limiter = standard.limiterSet.sugarLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.sugarLimit.limterType != LimiterType.None && defaultStandard.limiterSet.sugarLimit.calType != CalculateType.None)
                {
                    sugarScore.limiter = defaultStandard.limiterSet.sugarLimit;
                }
            }
            if (sugarScore.limiter != null)
            {
                if(sugarScore.limiter.calType == CalculateType.PercentEnergy)
                    sugarScore.value = dishNutr.sugars * 4;
                else
                    sugarScore.value = dishNutr.sugars;
                allScore.Add(sugarScore);
            }
        }

        if (standard.limiterSet.cholesterolLimit.weight > 0)
        {
            DishScoreHolder cholesScore = new DishScoreHolder();
            if (standard.limiterSet.cholesterolLimit.isTop4Priority == true)
                cholesScore.isPriority = true;
            if (standard.limiterSet.cholesterolLimit.limterType != LimiterType.None && standard.limiterSet.cholesterolLimit.calType != CalculateType.None)
                cholesScore.limiter = standard.limiterSet.cholesterolLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.cholesterolLimit.limterType != LimiterType.None && defaultStandard.limiterSet.cholesterolLimit.calType != CalculateType.None)
                {
                    cholesScore.limiter = defaultStandard.limiterSet.cholesterolLimit;
                }
            }
            if (cholesScore.limiter != null)
            {
                cholesScore.value = dishNutr.cholesterol;
                allScore.Add(cholesScore);
            }
        }

        if (standard.limiterSet.fiberLimit.weight > 0)
        {
            DishScoreHolder fiberScore = new DishScoreHolder();
            if (standard.limiterSet.fiberLimit.isTop4Priority == true)
                fiberScore.isPriority = true;
            if (standard.limiterSet.fiberLimit.limterType != LimiterType.None && standard.limiterSet.fiberLimit.calType != CalculateType.None)
                fiberScore.limiter = standard.limiterSet.fiberLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.fiberLimit.limterType != LimiterType.None && defaultStandard.limiterSet.fiberLimit.calType != CalculateType.None)
                {
                    fiberScore.limiter = defaultStandard.limiterSet.fiberLimit;
                }
            }
            if (fiberScore.limiter != null)
            {
                fiberScore.value = dishNutr.fiber;
                allScore.Add(fiberScore);
            }
        }

        if (standard.limiterSet.waterLimit.weight > 0)
        {
            DishScoreHolder waterScore = new DishScoreHolder();
            if (standard.limiterSet.waterLimit.isTop4Priority == true)
                waterScore.isPriority = true;
            if (standard.limiterSet.waterLimit.limterType != LimiterType.None && standard.limiterSet.waterLimit.calType != CalculateType.None)
                waterScore.limiter = standard.limiterSet.waterLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.waterLimit.limterType != LimiterType.None && defaultStandard.limiterSet.waterLimit.calType != CalculateType.None)
                {
                    waterScore.limiter = defaultStandard.limiterSet.waterLimit;
                }
            }
            if (waterScore.limiter != null)
            {
                waterScore.value = dishNutr.water;
                allScore.Add(waterScore);
            }
        }

        if (standard.limiterSet.PotassiumLimit.weight > 0)
        {
            DishScoreHolder potassScore = new DishScoreHolder();
            if (standard.limiterSet.PotassiumLimit.isTop4Priority == true)
                potassScore.isPriority = true;
            if (standard.limiterSet.PotassiumLimit.limterType != LimiterType.None && standard.limiterSet.PotassiumLimit.calType != CalculateType.None)
                potassScore.limiter = standard.limiterSet.PotassiumLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.PotassiumLimit.limterType != LimiterType.None && defaultStandard.limiterSet.PotassiumLimit.calType != CalculateType.None)
                {
                    potassScore.limiter = defaultStandard.limiterSet.PotassiumLimit;
                }
            }
            if (potassScore.limiter != null)
            {
                potassScore.value = dishNutr.potassium;
                allScore.Add(potassScore);
            }
        }

        if (standard.limiterSet.sodiumLimit.weight > 0)
        {
            DishScoreHolder sodiumScore = new DishScoreHolder();
            if (standard.limiterSet.sodiumLimit.isTop4Priority == true)
                sodiumScore.isPriority = true;
            if (standard.limiterSet.sodiumLimit.limterType != LimiterType.None && standard.limiterSet.sodiumLimit.calType != CalculateType.None)
                sodiumScore.limiter = standard.limiterSet.sodiumLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.sodiumLimit.limterType != LimiterType.None && defaultStandard.limiterSet.sodiumLimit.calType != CalculateType.None)
                {
                    sodiumScore.limiter = defaultStandard.limiterSet.sodiumLimit;
                }
            }
            if (sodiumScore.limiter != null)
            {
                sodiumScore.value = dishNutr.sodium;
                allScore.Add(sodiumScore);
            }
        }

        if (standard.limiterSet.calciumLimit.weight > 0)
        {
            DishScoreHolder calcScore = new DishScoreHolder();
            if (standard.limiterSet.calciumLimit.isTop4Priority == true)
                calcScore.isPriority = true;
            if (standard.limiterSet.calciumLimit.limterType != LimiterType.None && standard.limiterSet.calciumLimit.calType != CalculateType.None)
                calcScore.limiter = standard.limiterSet.calciumLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.calciumLimit.limterType != LimiterType.None && defaultStandard.limiterSet.calciumLimit.calType != CalculateType.None)
                {
                    calcScore.limiter = defaultStandard.limiterSet.calciumLimit;
                }
            }
            if (calcScore.limiter != null)
            {
                calcScore.value = dishNutr.calcium;
                allScore.Add(calcScore);
            }
        }

        if (standard.limiterSet.phosphorusLimit.weight > 0)
        {
            DishScoreHolder phospScore = new DishScoreHolder();
            if (standard.limiterSet.phosphorusLimit.isTop4Priority == true)
                phospScore.isPriority = true;
            if (standard.limiterSet.phosphorusLimit.limterType != LimiterType.None && standard.limiterSet.phosphorusLimit.calType != CalculateType.None)
                phospScore.limiter = standard.limiterSet.phosphorusLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.phosphorusLimit.limterType != LimiterType.None && defaultStandard.limiterSet.phosphorusLimit.calType != CalculateType.None)
                {
                    phospScore.limiter = defaultStandard.limiterSet.phosphorusLimit;
                }
            }
            if (phospScore.limiter != null)
            {
                phospScore.value = dishNutr.phosphorus;
                allScore.Add(phospScore);
            }
        }

        if (standard.limiterSet.magnesiumLimit.weight > 0)
        {
            DishScoreHolder magnScore = new DishScoreHolder();
            if (standard.limiterSet.magnesiumLimit.isTop4Priority == true)
                magnScore.isPriority = true;
            if (standard.limiterSet.magnesiumLimit.limterType != LimiterType.None && standard.limiterSet.magnesiumLimit.calType != CalculateType.None)
                magnScore.limiter = standard.limiterSet.magnesiumLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.magnesiumLimit.limterType != LimiterType.None && defaultStandard.limiterSet.magnesiumLimit.calType != CalculateType.None)
                {
                    magnScore.limiter = defaultStandard.limiterSet.magnesiumLimit;
                }
            }
            if (magnScore.limiter != null)
            {
                magnScore.value = dishNutr.magnesium;
                allScore.Add(magnScore);
            }
        }

        if (standard.limiterSet.zincLimit.weight > 0)
        {
            DishScoreHolder zincScore = new DishScoreHolder();
            if (standard.limiterSet.zincLimit.isTop4Priority == true)
                zincScore.isPriority = true;
            if (standard.limiterSet.zincLimit.limterType != LimiterType.None && standard.limiterSet.zincLimit.calType != CalculateType.None)
                zincScore.limiter = standard.limiterSet.zincLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.zincLimit.limterType != LimiterType.None && defaultStandard.limiterSet.zincLimit.calType != CalculateType.None)
                {
                    zincScore.limiter = defaultStandard.limiterSet.zincLimit;
                }
            }
            if (zincScore.limiter != null)
            {
                zincScore.value = dishNutr.zinc;
                allScore.Add(zincScore);
            }
        }

        if (standard.limiterSet.ironLimit.weight > 0)
        {
            DishScoreHolder ironScore = new DishScoreHolder();
            if (standard.limiterSet.ironLimit.isTop4Priority == true)
                ironScore.isPriority = true;
            if (standard.limiterSet.ironLimit.limterType != LimiterType.None && standard.limiterSet.ironLimit.calType != CalculateType.None)
                ironScore.limiter = standard.limiterSet.ironLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.ironLimit.limterType != LimiterType.None && defaultStandard.limiterSet.ironLimit.calType != CalculateType.None)
                {
                    ironScore.limiter = defaultStandard.limiterSet.ironLimit;
                }
            }
            if (ironScore.limiter != null)
            {
                ironScore.value = dishNutr.iron;
                allScore.Add(ironScore);
            }
        }

        if (standard.limiterSet.manganeseLimit.weight > 0)
        {
            DishScoreHolder mangScore = new DishScoreHolder();
            if (standard.limiterSet.manganeseLimit.isTop4Priority == true)
                mangScore.isPriority = true;
            if (standard.limiterSet.manganeseLimit.limterType != LimiterType.None && standard.limiterSet.manganeseLimit.calType != CalculateType.None)
                mangScore.limiter = standard.limiterSet.manganeseLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.manganeseLimit.limterType != LimiterType.None && defaultStandard.limiterSet.manganeseLimit.calType != CalculateType.None)
                {
                    mangScore.limiter = defaultStandard.limiterSet.manganeseLimit;
                }
            }
            if (mangScore.limiter != null)
            {
                mangScore.value = dishNutr.manganese;
                allScore.Add(mangScore);
            }
        }

        if (standard.limiterSet.copperLimit.weight > 0)
        {
            DishScoreHolder coppScore = new DishScoreHolder();
            if (standard.limiterSet.copperLimit.isTop4Priority == true)
                coppScore.isPriority = true;
            if (standard.limiterSet.copperLimit.limterType != LimiterType.None && standard.limiterSet.copperLimit.calType != CalculateType.None)
                coppScore.limiter = standard.limiterSet.copperLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.copperLimit.limterType != LimiterType.None && defaultStandard.limiterSet.copperLimit.calType != CalculateType.None)
                {
                    coppScore.limiter = defaultStandard.limiterSet.copperLimit;
                }
            }
            if (coppScore.limiter != null)
            {
                coppScore.value = dishNutr.copper;
                allScore.Add(coppScore);
            }
        }

        if (standard.limiterSet.seleniumLimit.weight > 0)
        {
            DishScoreHolder seleScore = new DishScoreHolder();
            if (standard.limiterSet.seleniumLimit.isTop4Priority == true)
                seleScore.isPriority = true;
            if (standard.limiterSet.seleniumLimit.limterType != LimiterType.None && standard.limiterSet.seleniumLimit.calType != CalculateType.None)
                seleScore.limiter = standard.limiterSet.seleniumLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.seleniumLimit.limterType != LimiterType.None && defaultStandard.limiterSet.seleniumLimit.calType != CalculateType.None)
                {
                    seleScore.limiter = defaultStandard.limiterSet.seleniumLimit;
                }
            }
            if (seleScore.limiter != null)
            {
                seleScore.value = dishNutr.selenium;
                allScore.Add(seleScore);
            }
        }

        if (standard.limiterSet.vitaminALimit.weight > 0)
        {
            DishScoreHolder aScore = new DishScoreHolder();
            if (standard.limiterSet.vitaminALimit.isTop4Priority == true)
                aScore.isPriority = true;
            if (standard.limiterSet.vitaminALimit.limterType != LimiterType.None && standard.limiterSet.vitaminALimit.calType != CalculateType.None)
                aScore.limiter = standard.limiterSet.vitaminALimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminALimit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminALimit.calType != CalculateType.None)
                {
                    aScore.limiter = defaultStandard.limiterSet.vitaminALimit;
                }
            }
            if (aScore.limiter != null)
            {
                aScore.value = dishNutr.vitaminA;
                allScore.Add(aScore);
            }
        }

        if (standard.limiterSet.vitaminB1Limit.weight > 0)
        {
            DishScoreHolder b1Score = new DishScoreHolder();
            if (standard.limiterSet.vitaminB1Limit.isTop4Priority == true)
                b1Score.isPriority = true;
            if (standard.limiterSet.vitaminB1Limit.limterType != LimiterType.None && standard.limiterSet.vitaminB1Limit.calType != CalculateType.None)
                b1Score.limiter = standard.limiterSet.vitaminB1Limit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminB1Limit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminB1Limit.calType != CalculateType.None)
                {
                    b1Score.limiter = defaultStandard.limiterSet.vitaminB1Limit;
                }
            }
            if (b1Score.limiter != null)
            {
                b1Score.value = dishNutr.vitaminB1;
                allScore.Add(b1Score);
            }
        }

        if (standard.limiterSet.vitaminB2Limit.weight > 0)
        {
            DishScoreHolder b2Score = new DishScoreHolder();
            if (standard.limiterSet.vitaminB2Limit.isTop4Priority == true)
                b2Score.isPriority = true;
            if (standard.limiterSet.vitaminB2Limit.limterType != LimiterType.None && standard.limiterSet.vitaminB2Limit.calType != CalculateType.None)
                b2Score.limiter = standard.limiterSet.vitaminB2Limit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminB2Limit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminB2Limit.calType != CalculateType.None)
                {
                    b2Score.limiter = defaultStandard.limiterSet.vitaminB2Limit;
                }
            }
            if (b2Score.limiter != null)
            {
                b2Score.value = dishNutr.vitaminB2;
                allScore.Add(b2Score);
            }
        }

        if (standard.limiterSet.vitaminB3Limit.weight > 0)
        {
            DishScoreHolder b3Score = new DishScoreHolder();
            if (standard.limiterSet.vitaminB3Limit.isTop4Priority == true)
                b3Score.isPriority = true;
            if (standard.limiterSet.vitaminB3Limit.limterType != LimiterType.None && standard.limiterSet.vitaminB3Limit.calType != CalculateType.None)
                b3Score.limiter = standard.limiterSet.vitaminB3Limit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminB3Limit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminB3Limit.calType != CalculateType.None)
                {
                    b3Score.limiter = defaultStandard.limiterSet.vitaminB3Limit;
                }
            }
            if (b3Score.limiter != null)
            {
                b3Score.value = dishNutr.vitaminB3;
                allScore.Add(b3Score);
            }
        }

        if (standard.limiterSet.vitaminB5Limit.weight > 0)
        {
            DishScoreHolder b5Score = new DishScoreHolder();
            if (standard.limiterSet.vitaminB5Limit.isTop4Priority == true)
                b5Score.isPriority = true;
            if (standard.limiterSet.vitaminB5Limit.limterType != LimiterType.None && standard.limiterSet.vitaminB5Limit.calType != CalculateType.None)
                b5Score.limiter = standard.limiterSet.vitaminB5Limit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminB5Limit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminB5Limit.calType != CalculateType.None)
                {
                    b5Score.limiter = defaultStandard.limiterSet.vitaminB5Limit;
                }
            }
            if (b5Score.limiter != null)
            {
                b5Score.value = dishNutr.vitaminB5;
                allScore.Add(b5Score);
            }
        }

        if (standard.limiterSet.vitaminB6Limit.weight > 0)
        {
            DishScoreHolder b6Score = new DishScoreHolder();
            if (standard.limiterSet.vitaminB6Limit.isTop4Priority == true)
                b6Score.isPriority = true;
            if (standard.limiterSet.vitaminB6Limit.limterType != LimiterType.None && standard.limiterSet.vitaminB6Limit.calType != CalculateType.None)
                b6Score.limiter = standard.limiterSet.vitaminB6Limit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminB6Limit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminB6Limit.calType != CalculateType.None)
                {
                    b6Score.limiter = defaultStandard.limiterSet.vitaminB6Limit;
                }
            }
            if (b6Score.limiter != null)
            {
                b6Score.value = dishNutr.vitaminB6;
                allScore.Add(b6Score);
            }
        }

        if (standard.limiterSet.vitaminB7Limit.weight > 0)
        {
            DishScoreHolder b7Score = new DishScoreHolder();
            if (standard.limiterSet.vitaminB7Limit.isTop4Priority == true)
                b7Score.isPriority = true;
            if (standard.limiterSet.vitaminB7Limit.limterType != LimiterType.None && standard.limiterSet.vitaminB7Limit.calType != CalculateType.None)
                b7Score.limiter = standard.limiterSet.vitaminB7Limit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminB7Limit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminB7Limit.calType != CalculateType.None)
                {
                    b7Score.limiter = defaultStandard.limiterSet.vitaminB7Limit;
                }
            }
            if (b7Score.limiter != null)
            {
                b7Score.value = dishNutr.vitaminB7;
                allScore.Add(b7Score);
            }
        }

        if (standard.limiterSet.vitaminB9Limit.weight > 0)
        {
            DishScoreHolder b9Score = new DishScoreHolder();
            if (standard.limiterSet.vitaminB9Limit.isTop4Priority == true)
                b9Score.isPriority = true;
            if (standard.limiterSet.vitaminB9Limit.limterType != LimiterType.None && standard.limiterSet.vitaminB9Limit.calType != CalculateType.None)
                b9Score.limiter = standard.limiterSet.vitaminB9Limit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminB9Limit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminB9Limit.calType != CalculateType.None)
                {
                    b9Score.limiter = defaultStandard.limiterSet.vitaminB9Limit;
                }
            }
            if (b9Score.limiter != null)
            {
                b9Score.value = dishNutr.vitaminB9;
                allScore.Add(b9Score);
            }
        }

        if (standard.limiterSet.vitaminB12Limit.weight > 0)
        {
            DishScoreHolder b12Score = new DishScoreHolder();
            if (standard.limiterSet.vitaminB12Limit.isTop4Priority == true)
                b12Score.isPriority = true;
            if (standard.limiterSet.vitaminB12Limit.limterType != LimiterType.None && standard.limiterSet.vitaminB12Limit.calType != CalculateType.None)
                b12Score.limiter = standard.limiterSet.vitaminB12Limit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminB12Limit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminB12Limit.calType != CalculateType.None)
                {
                    b12Score.limiter = defaultStandard.limiterSet.vitaminB12Limit;
                }
            }
            if (b12Score.limiter != null)
            {
                b12Score.value = dishNutr.vitaminB12;
                allScore.Add(b12Score);
            }
        }

        if (standard.limiterSet.vitaminCLimit.weight > 0)
        {
            DishScoreHolder cScore = new DishScoreHolder();
            if (standard.limiterSet.vitaminCLimit.isTop4Priority == true)
                cScore.isPriority = true;
            if (standard.limiterSet.vitaminCLimit.limterType != LimiterType.None && standard.limiterSet.vitaminCLimit.calType != CalculateType.None)
                cScore.limiter = standard.limiterSet.vitaminCLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminCLimit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminCLimit.calType != CalculateType.None)
                {
                    cScore.limiter = defaultStandard.limiterSet.vitaminCLimit;
                }
            }
            if (cScore.limiter != null)
            {
                cScore.value = dishNutr.vitaminC;
                allScore.Add(cScore);
            }
        }

        if (standard.limiterSet.vitaminDLimit.weight > 0)
        {
            DishScoreHolder dScore = new DishScoreHolder();
            if (standard.limiterSet.vitaminDLimit.isTop4Priority == true)
                dScore.isPriority = true;
            if (standard.limiterSet.vitaminDLimit.limterType != LimiterType.None && standard.limiterSet.vitaminDLimit.calType != CalculateType.None)
                dScore.limiter = standard.limiterSet.vitaminDLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminDLimit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminDLimit.calType != CalculateType.None)
                {
                    dScore.limiter = defaultStandard.limiterSet.vitaminDLimit;
                }
            }
            if (dScore.limiter != null)
            {
                dScore.value = dishNutr.vitaminD;
                allScore.Add(dScore);
            }
        }

        if (standard.limiterSet.vitaminELimit.weight > 0)
        {
            DishScoreHolder eScore = new DishScoreHolder();
            if (standard.limiterSet.vitaminELimit.isTop4Priority == true)
                eScore.isPriority = true;
            if (standard.limiterSet.vitaminELimit.limterType != LimiterType.None && standard.limiterSet.vitaminELimit.calType != CalculateType.None)
                eScore.limiter = standard.limiterSet.vitaminELimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminELimit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminELimit.calType != CalculateType.None)
                {
                    eScore.limiter = defaultStandard.limiterSet.vitaminELimit;
                }
            }
            if (eScore.limiter != null)
            {
                eScore.value = dishNutr.vitaminE;
                allScore.Add(eScore);
            }
        }

        if (standard.limiterSet.vitaminKLimit.weight > 0)
        {
            DishScoreHolder kScore = new DishScoreHolder();
            if (standard.limiterSet.vitaminKLimit.isTop4Priority == true)
                kScore.isPriority = true;
            if (standard.limiterSet.vitaminKLimit.limterType != LimiterType.None && standard.limiterSet.vitaminKLimit.calType != CalculateType.None)
                kScore.limiter = standard.limiterSet.vitaminKLimit;
            else if (defaultStandard != null)
            {
                if (defaultStandard.limiterSet.vitaminKLimit.limterType != LimiterType.None && defaultStandard.limiterSet.vitaminKLimit.calType != CalculateType.None)
                {
                    kScore.limiter = defaultStandard.limiterSet.vitaminKLimit;
                }
            }
            if (kScore.limiter != null)
            {
                kScore.value = dishNutr.vitaminK;
                allScore.Add(kScore);
            }
        }



        foreach (var scoreHolder in allScore)
        {
            if(scoreHolder.limiter.weight > 0)
            {
                switch(scoreHolder.limiter.calType)
                {
                    case CalculateType.BaseTotalEnergy:
                        CalculateScore(scoreHolder);
                        break;
                    case CalculateType.PercentEnergy:
                        CalculateScoreByEnergy(scoreHolder, totalEnergy);
                        break;
                    case CalculateType.Mass_Gram:
                        CalculateScore(scoreHolder);
                        break;
                }
            }
        }

        resultScore.allScore = allScore;
        resultScore.totalEnergy = totalEnergy;
        CalculateFinalScore(resultScore);
        
        return resultScore;
    }

    public void CalculateFinalScore(ResultScore resultScoreHolder)
    {
        float totalWeight = 0;
        float totalScore = 0;
        foreach(var scoreHolder in resultScoreHolder.allScore)
        {
            if (scoreHolder.limiter != null)
            {
                if (scoreHolder.limiter.weight > 0)
                {
                    totalWeight += scoreHolder.limiter.weight;
                    totalScore += scoreHolder.limiter.weight * scoreHolder.actualScore;
                }
            }
        }

        var finalScore = Mathf.Ceil(totalScore / totalWeight);
        resultScoreHolder.finalScore = finalScore;

        if (finalScore >= star5) resultScoreHolder.finalStar = 5;
        else if (finalScore >= star4) resultScoreHolder.finalStar = 4;
        else if (finalScore >= star3) resultScoreHolder.finalStar = 3;
        else if (finalScore >= star2) resultScoreHolder.finalStar = 2;
        else if (finalScore > star1) resultScoreHolder.finalStar = 1;
        else resultScoreHolder.finalStar = 0;
    }

    public void CalculateScore(DishScoreHolder scoreHolder)
    {
        Limiter limiter = scoreHolder.limiter;

        switch (scoreHolder.limiter.calType)
        {
            case CalculateType.Mass_Gram:
                {
                    scoreHolder.value *= 0.001f;
                    break;
                }
            case CalculateType.Mass_Microgram:
                {
                    scoreHolder.value *= 1000;
                    break;
                }
            default: //Mass_Miligram, Mass_Vitamin
                //do nothing
                break;
        }

        switch (scoreHolder.limiter.limterType)
        {
            case LimiterType.InLimiterBest:
                {
                    if (limiter.lowerLimit != -1 && limiter.upperLimit != -1)
                    {
                        var alphaValue = (limiter.upperLimit + limiter.lowerLimit) / 2 * limiter.alpha;
                        var fiveStarLowerLimit = limiter.lowerLimit + alphaValue;
                        var fiveStarUpperLimit = limiter.upperLimit - alphaValue;
                        if (scoreHolder.value >= fiveStarLowerLimit && scoreHolder.value <= fiveStarUpperLimit)
                        {
                            scoreHolder.star = 5;
                            var median = (fiveStarLowerLimit + fiveStarUpperLimit) / 2;
                            if (scoreHolder.value < median)
                            {
                                var lower = fiveStarLowerLimit;
                                scoreHolder.actualScore = star5 + (scoreHolder.value - lower) / (median - lower) * (100f - star5);
                            }
                            else
                            {
                                var upper = fiveStarUpperLimit;
                                scoreHolder.actualScore = star5 + (upper - scoreHolder.value) / (upper - median) * (100f - star5);
                            }
                        }
                        else if (scoreHolder.value >= limiter.lowerLimit && scoreHolder.value <= limiter.upperLimit)
                        {
                            scoreHolder.star = 4;
                            if (scoreHolder.value < fiveStarLowerLimit)
                            {
                                var lower = limiter.lowerLimit;
                                var upper = fiveStarLowerLimit;
                                scoreHolder.actualScore = star4 + (scoreHolder.value - lower) / (upper - lower) * (star5 - star4);
                            }
                            else //scoreHolder.value > maxScoreUpperLimit
                            {
                                var lower = limiter.upperLimit;
                                var upper = fiveStarUpperLimit;
                                scoreHolder.actualScore = star5 + (upper - scoreHolder.value) / (upper - lower) * (star5 - star4);
                            }
                        }
                        else
                        {
                            var betaValue = limiter.lowerLimit * (1 - limiter.beta);
                            var zeroStarLowerLimit = limiter.lowerLimit - betaValue;
                            var zeroStarUpperLimit = limiter.upperLimit + betaValue;

                            if (zeroStarLowerLimit < 0) zeroStarLowerLimit = 0;

                            if (scoreHolder.value < zeroStarLowerLimit || scoreHolder.value > zeroStarUpperLimit)
                            {
                                scoreHolder.star = 0;
                                scoreHolder.actualScore = 0;
                            }
                            else
                            {
                                if (scoreHolder.value < limiter.lowerLimit)
                                {
                                    var lower = zeroStarLowerLimit;
                                    var upper = limiter.lowerLimit;
                                    scoreHolder.actualScore = (scoreHolder.value - lower) / (upper - lower) * (star4 - star1);
                                }
                                else if (scoreHolder.value > limiter.upperLimit)
                                {
                                    var lower = limiter.upperLimit;
                                    var upper = zeroStarUpperLimit;
                                    scoreHolder.actualScore = (upper - scoreHolder.value) / (upper - lower) * (star4 - star1);
                                }

                                if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                                else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                                else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                                else scoreHolder.star = 0;
                            }
                        }
                        if (scoreHolder.value < limiter.lowerLimit)
                        {
                            scoreHolder.detail = "< " + limiter.lowerLimit;
                        }
                        else if (scoreHolder.value > limiter.upperLimit)
                        {
                            scoreHolder.detail = "> " + limiter.upperLimit;
                        }
                        else
                        {
                            scoreHolder.detail = $"= {limiter.lowerLimit} ~ {limiter.upperLimit}";
                        }
                    }
                    else if (limiter.median != -1)
                    {
                        var alphaValue = limiter.median * limiter.alpha;
                        var fiveStarLowerLimit = limiter.median - alphaValue;
                        var fiveStarUpperLimit = limiter.median + alphaValue;
                        if (scoreHolder.value >= fiveStarLowerLimit && scoreHolder.value <= fiveStarUpperLimit)
                        {
                            scoreHolder.star = 5;
                            var median = limiter.median;
                            if (scoreHolder.value < median)
                            {
                                var lower = fiveStarLowerLimit;
                                scoreHolder.actualScore = star5 + (scoreHolder.value - lower) / (median - lower) * (100f - star5);
                            }
                            else
                            {
                                var upper = fiveStarUpperLimit;
                                scoreHolder.actualScore = star5 + (upper - scoreHolder.value) / (upper - median) * (100f - star5);
                            }
                        }
                        else
                        {
                            var betaValue = limiter.median * (1 - limiter.beta);
                            var zeroStarLowerLimit = limiter.median - betaValue;
                            var zeroStarUpperLimit = limiter.median + betaValue;

                            if (zeroStarLowerLimit < 0) zeroStarLowerLimit = 0;

                            if (scoreHolder.value < zeroStarLowerLimit || scoreHolder.value > zeroStarUpperLimit)
                            {
                                scoreHolder.star = 0;
                                scoreHolder.actualScore = 0;
                            }
                            else
                            {
                                if (scoreHolder.value < limiter.lowerLimit)
                                {
                                    var lower = zeroStarLowerLimit;
                                    var upper = limiter.lowerLimit;
                                    scoreHolder.actualScore = star1 + (lower - scoreHolder.value) / (lower - upper) * (star5 - star1);
                                }
                                else if (scoreHolder.value > limiter.upperLimit)
                                {
                                    var lower = limiter.upperLimit;
                                    var upper = zeroStarUpperLimit;
                                    scoreHolder.actualScore = star1 + (upper - scoreHolder.value) / (upper - lower) * (star5 - star1);
                                }

                                if (scoreHolder.actualScore >= star4) scoreHolder.star = 4;
                                else if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                                else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                                else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                                else scoreHolder.star = 0;
                            }
                        }
                        if (scoreHolder.value < limiter.median)
                        {
                            scoreHolder.detail = $"Good Value: {limiter.median} (<)";
                        }
                        else if (scoreHolder.value > limiter.median)
                        {
                            scoreHolder.detail = $"Good Value: {limiter.median} (>)";
                        }
                        else
                        {
                            scoreHolder.detail = $"Good Value: {limiter.median} (=)";
                        }
                    }
                    //else if(limiter.median == -1 && limiter.lowerLimit != -1 && limiter.upperLimit != -1)
                    //{

                    //}
                    break;
                }
            case LimiterType.MoreIsBetter:
                {
                    if (limiter.lowerLimit != -1)
                    {
                        var zeroStarLowerLimit = limiter.lowerLimit * limiter.beta;
                        var fiveStarLowerLimit = limiter.lowerLimit + (limiter.lowerLimit * limiter.alpha);

                        if (zeroStarLowerLimit < 0) zeroStarLowerLimit = 0;

                        if (scoreHolder.value >= fiveStarLowerLimit)
                        {
                            scoreHolder.star = 5;
                            var lower = fiveStarLowerLimit;
                            var upper = fiveStarLowerLimit + (fiveStarLowerLimit - limiter.lowerLimit);
                            scoreHolder.actualScore = star5 + (scoreHolder.value - lower) / (upper - lower) * (100f - star5);
                        }
                        else if (scoreHolder.value >= limiter.lowerLimit)
                        {
                            scoreHolder.star = 4;
                            var lower = limiter.lowerLimit;
                            var upper = fiveStarLowerLimit;
                            scoreHolder.actualScore = star4 + (scoreHolder.value - lower) / (upper - lower) * (star5 - star4);
                        }
                        else if (scoreHolder.value < zeroStarLowerLimit)
                        {
                            scoreHolder.star = 0;
                            scoreHolder.actualScore = 0;
                        }
                        else //scoreHolder.value < limiter.lowerLimit
                        {
                            var lower = zeroStarLowerLimit;
                            var upper = limiter.lowerLimit;
                            scoreHolder.actualScore = (scoreHolder.value - lower) / (upper - lower) * (star4 - star1);

                            if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                            else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                            else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                            else scoreHolder.star = 0;
                        }
                        if (scoreHolder.value < limiter.lowerLimit)
                        {
                            scoreHolder.detail = "< " + limiter.lowerLimit;
                        }
                        else if (scoreHolder.value > limiter.lowerLimit)
                        {
                            scoreHolder.detail = "> " + limiter.lowerLimit;
                        }
                        else
                        {
                            scoreHolder.detail = $"= {limiter.lowerLimit}";
                        }
                    }
                    break;
                }
            case LimiterType.LessIsBetter:
                {
                    if (limiter.upperLimit != -1)
                    {
                        var fiveStarUpperLimit = limiter.upperLimit * (1 - limiter.alpha);
                        var zeroStarUpperLimit = limiter.upperLimit + (limiter.upperLimit * limiter.beta);
                        if (scoreHolder.value <= fiveStarUpperLimit)
                        {
                            scoreHolder.star = 5;
                            var lower = fiveStarUpperLimit - (limiter.upperLimit - fiveStarUpperLimit);
                            var upper = fiveStarUpperLimit;
                            scoreHolder.actualScore = star5 + (upper - scoreHolder.value) / (upper - lower) * (100f - star5);
                        }
                        else if (scoreHolder.value <= limiter.upperLimit)
                        {
                            scoreHolder.star = 4;
                            var lower = limiter.upperLimit;
                            var upper = fiveStarUpperLimit;
                            scoreHolder.actualScore = star5 + (upper - scoreHolder.value) / (upper - lower) * (star5 - star4);

                        }
                        else
                        {
                            if (scoreHolder.value > zeroStarUpperLimit)
                            {
                                scoreHolder.star = 0;
                                scoreHolder.actualScore = 0;
                            }
                            else
                            {
                                //scoreHolder.value > limiter.upperLimit

                                var lower = limiter.upperLimit;
                                var upper = zeroStarUpperLimit;
                                scoreHolder.actualScore = (upper - scoreHolder.value) / (upper - lower) * (star4 - star1);

                                if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                                else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                                else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                                else scoreHolder.star = 0;
                            }
                        }
                        if (scoreHolder.value < limiter.upperLimit)
                        {
                            scoreHolder.detail = "< " + limiter.upperLimit;
                        }
                        else if (scoreHolder.value > limiter.upperLimit)
                        {
                            scoreHolder.detail = "> " + limiter.upperLimit;
                        }
                        else
                        {
                            scoreHolder.detail = $"= {limiter.upperLimit}";
                        }
                    }
                    break;
                }
            default:
                break;
        }

        scoreHolder.actualScore = Mathf.Ceil(scoreHolder.actualScore);

        if (scoreHolder.actualScore > 100)
            scoreHolder.actualScore = 100;
        else if (scoreHolder.actualScore < 0)
            scoreHolder.actualScore = 0;
    }

    public void CalculateScoreByEnergy(DishScoreHolder scoreHolder, float totalEnergy)
    {
        if(totalEnergy <= 0)
        {
            return;
        }
        //Calculate by PercentEnergy Concept
        Limiter limiter = scoreHolder.limiter;
        var percentEnergy = scoreHolder.value / totalEnergy;
        switch (scoreHolder.limiter.limterType)
        {
            case LimiterType.InLimiterBest:
                {
                    if (limiter.lowerLimit != -1 && limiter.upperLimit != -1)
                    {
                        var alphaValue = (limiter.upperLimit + limiter.lowerLimit) / 2 * limiter.alpha;
                        var fiveStarLowerLimit = limiter.lowerLimit + alphaValue;
                        var fiveStarUpperLimit = limiter.upperLimit - alphaValue;
                        if (percentEnergy >= fiveStarLowerLimit && percentEnergy <= fiveStarUpperLimit)
                        {
                            scoreHolder.star = 5;
                            var median = (fiveStarLowerLimit + fiveStarUpperLimit) / 2;
                            if (percentEnergy < median)
                            {
                                var lower = fiveStarLowerLimit;
                                scoreHolder.actualScore = star5 + (percentEnergy - lower) / (median - lower) * (100f - star5);
                            }
                            else
                            {
                                var upper = fiveStarUpperLimit;
                                scoreHolder.actualScore = star5 + (upper - percentEnergy) / (upper - median) * (100f - star5);
                            }
                        }
                        else if (percentEnergy >= limiter.lowerLimit && percentEnergy <= limiter.upperLimit)
                        {
                            scoreHolder.star = 4;
                            if (percentEnergy < fiveStarLowerLimit)
                            {
                                var lower = limiter.lowerLimit;
                                var upper = fiveStarLowerLimit;
                                scoreHolder.actualScore = star4 + (percentEnergy - lower) / (upper - lower) * (star5 - star4);
                            }
                            else //scoreHolder.value > maxScoreUpperLimit
                            {
                                var lower = limiter.upperLimit;
                                var upper = fiveStarUpperLimit;
                                scoreHolder.actualScore = star5 + (upper - percentEnergy) / (upper - lower) * (star5 - star4);
                            }
                        }
                        else
                        {
                            var betaValue = limiter.lowerLimit * (1 - limiter.beta);
                            var zeroStarLowerLimit = limiter.lowerLimit - betaValue;
                            var zeroStarUpperLimit = limiter.upperLimit + betaValue;

                            if (zeroStarLowerLimit < 0) zeroStarLowerLimit = 0;

                            if (percentEnergy < zeroStarLowerLimit || percentEnergy > zeroStarUpperLimit)
                            {
                                scoreHolder.star = 0;
                                scoreHolder.actualScore = 0;
                            }
                            else
                            {
                                if (percentEnergy < limiter.lowerLimit)
                                {
                                    var lower = zeroStarLowerLimit;
                                    var upper = limiter.lowerLimit;
                                    scoreHolder.actualScore = (percentEnergy - lower) / (upper - lower) * (star4 - star1);
                                }
                                else if (percentEnergy > limiter.upperLimit)
                                {
                                    var lower = limiter.upperLimit;
                                    var upper = zeroStarUpperLimit;
                                    scoreHolder.actualScore = (upper - percentEnergy) / (upper - lower) * (star4 - star1);
                                }

                                if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                                else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                                else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                                else scoreHolder.star = 0;
                            }
                        }
                        scoreHolder.detail = $"Good Value: {limiter.lowerLimit * totalEnergy} ~ {limiter.upperLimit * totalEnergy}";
                    }
                    else if (limiter.median != -1)
                    {
                        var alphaValue = limiter.median * limiter.alpha;
                        var fiveStarLowerLimit = limiter.median - alphaValue;
                        var fiveStarUpperLimit = limiter.median + alphaValue;
                        if (percentEnergy >= fiveStarLowerLimit && percentEnergy <= fiveStarUpperLimit)
                        {
                            scoreHolder.star = 5;
                            var median = limiter.median;
                            if (percentEnergy < median)
                            {
                                var lower = fiveStarLowerLimit;
                                scoreHolder.actualScore = star5 + (percentEnergy - lower) / (median - lower) * (100f - star5);
                            }
                            else
                            {
                                var upper = fiveStarUpperLimit;
                                scoreHolder.actualScore = star5 + (percentEnergy) / (upper - median) * (100f - star5);
                            }
                        }
                        else
                        {
                            var betaValue = limiter.median * (1 - limiter.beta);
                            var zeroStarLowerLimit = limiter.median - betaValue;
                            var zeroStarUpperLimit = limiter.median + betaValue;

                            if (zeroStarLowerLimit < 0) zeroStarLowerLimit = 0;

                            if (percentEnergy < zeroStarLowerLimit || percentEnergy > zeroStarUpperLimit)
                            {
                                scoreHolder.star = 0;
                                scoreHolder.actualScore = 0;
                            }
                            else
                            {
                                if (percentEnergy <= fiveStarLowerLimit)
                                {
                                    var lower = zeroStarLowerLimit;
                                    var upper = limiter.lowerLimit;
                                    scoreHolder.actualScore = (percentEnergy - lower) / (upper - lower) * (star5 - star1);
                                }
                                else if (percentEnergy >= fiveStarUpperLimit)
                                {
                                    var lower = limiter.upperLimit;
                                    var upper = zeroStarUpperLimit;
                                    scoreHolder.actualScore = star1 + (upper - percentEnergy) / (upper - lower) * (star5 - star1);
                                }

                                if (scoreHolder.actualScore >= star4) scoreHolder.star = 4;
                                else if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                                else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                                else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                                else scoreHolder.star = 0;
                            }
                        }

                        scoreHolder.detail = $"Good Value: {limiter.median * totalEnergy}";
                    }
                    //else if(limiter.median == -1 && limiter.lowerLimit != -1 && limiter.upperLimit != -1)
                    //{

                    //}
                    break;
                }
            case LimiterType.MoreIsBetter:
                {
                    if (limiter.lowerLimit != -1)
                    {
                        var zeroStarLowerLimit = limiter.lowerLimit * limiter.beta;
                        var fiveStarLowerLimit = limiter.lowerLimit + (limiter.lowerLimit * limiter.alpha);

                        if (zeroStarLowerLimit < 0) zeroStarLowerLimit = 0;

                        if (percentEnergy >= fiveStarLowerLimit)
                        {
                            scoreHolder.star = 5;
                            var lower = fiveStarLowerLimit;
                            var upper = fiveStarLowerLimit + (fiveStarLowerLimit - limiter.lowerLimit);
                            scoreHolder.actualScore = star5 + (percentEnergy - lower) / (upper - lower) * (100f - star5);
                        }
                        else if (percentEnergy >= limiter.lowerLimit)
                        {
                            scoreHolder.star = 4;
                            var lower = limiter.lowerLimit;
                            var upper = fiveStarLowerLimit;
                            scoreHolder.actualScore = star4 + (percentEnergy - lower) / (upper - lower) * (star5 - star4);
                        }
                        else if (percentEnergy < zeroStarLowerLimit)
                        {
                            scoreHolder.star = 0;
                            scoreHolder.actualScore = 0;
                        }
                        else //scoreHolder.value < limiter.lowerLimit
                        {
                            var lower = zeroStarLowerLimit;
                            var upper = limiter.lowerLimit;
                            scoreHolder.actualScore = (percentEnergy - lower) / (upper - lower) * (star4 - star1);

                            if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                            else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                            else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                            else scoreHolder.star = 0;
                        }

                        scoreHolder.detail = $"Good Value: < {limiter.lowerLimit * totalEnergy}";
                    }
                    break;
                }
            case LimiterType.LessIsBetter:
                {
                    if (limiter.upperLimit != -1)
                    {
                        var fiveStarUpperLimit = limiter.upperLimit * (1 - limiter.alpha);
                        var zeroStarUpperLimit = limiter.upperLimit + (limiter.upperLimit * limiter.beta);
                        if (percentEnergy <= fiveStarUpperLimit)
                        {
                            scoreHolder.star = 5;
                            var lower = fiveStarUpperLimit - (limiter.upperLimit - fiveStarUpperLimit);
                            var upper = fiveStarUpperLimit;
                            scoreHolder.actualScore = star5 + (upper - percentEnergy) / (upper - lower) * (100f - star5);
                        }
                        else if (percentEnergy <= limiter.upperLimit)
                        {
                            scoreHolder.star = 4;
                            var lower = limiter.upperLimit;
                            var upper = fiveStarUpperLimit;
                            scoreHolder.actualScore = star5 + (upper - percentEnergy) / (upper - lower) * (star5 - star4);

                        }
                        else
                        {
                            if (percentEnergy > zeroStarUpperLimit)
                            {
                                scoreHolder.star = 0;
                                scoreHolder.actualScore = 0;
                            }
                            else
                            {
                                //scoreHolder.value > limiter.upperLimit

                                var lower = limiter.upperLimit;
                                var upper = zeroStarUpperLimit;
                                scoreHolder.actualScore = (upper - percentEnergy) / (upper - lower) * (star4 - star1);

                                if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                                else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                                else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                                else scoreHolder.star = 0;
                            }
                        }

                        scoreHolder.detail = $"Good Value: < {limiter.upperLimit * totalEnergy}";

                    }
                    break;
                }
            default:
                break;
        }

        scoreHolder.actualScore = Mathf.Ceil(scoreHolder.actualScore);

        if (scoreHolder.actualScore > 100)
            scoreHolder.actualScore = 100;
        else if (scoreHolder.actualScore < 0)
            scoreHolder.actualScore = 0;


    }


    public void SetZeroNutr(Nutrition nutr)
    {
        nutr.cholesterol = 0;
        nutr.sugars = 0;
        nutr.fiber = 0;
        nutr.proteins = 0;
        nutr.fat = 0;
        nutr.saturatedfat = 0;
        nutr.water = 0;
        nutr.potassium = 0;
        nutr.sodium = 0;
        nutr.calcium = 0;
        nutr.phosphorus = 0;
        nutr.magnesium = 0;
        nutr.zinc = 0;
        nutr.iron = 0;
        nutr.manganese = 0;
        nutr.copper = 0;
        nutr.selenium = 0;
        nutr.vitaminB1 = 0;
        nutr.vitaminB2 = 0;
        nutr.vitaminB3 = 0;
        nutr.vitaminB5 = 0;
        nutr.vitaminB6 = 0;
        nutr.vitaminB7 = 0;
        nutr.vitaminB9 = 0;
        nutr.vitaminB12 = 0;
        nutr.vitaminC = 0;
        nutr.vitaminA = 0;
        nutr.vitaminD = 0;
        nutr.vitaminE = 0;
        nutr.vitaminK = 0;
    }

    public void SumNitr(Nutrition dishNutr, List<IngredientObject> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            dishNutr.cholesterol += ingredient.nutrition.cholesterol;
            dishNutr.sugars += ingredient.nutrition.sugars;
            dishNutr.fiber += ingredient.nutrition.fiber;
            dishNutr.proteins += ingredient.nutrition.proteins;
            dishNutr.fat += ingredient.nutrition.fat;
            dishNutr.saturatedfat += ingredient.nutrition.saturatedfat;
            dishNutr.water += ingredient.nutrition.water;
            dishNutr.potassium += ingredient.nutrition.potassium;
            dishNutr.sodium += ingredient.nutrition.sodium;
            dishNutr.calcium += ingredient.nutrition.calcium;
            dishNutr.phosphorus += ingredient.nutrition.phosphorus;
            dishNutr.magnesium += ingredient.nutrition.magnesium;
            dishNutr.zinc += ingredient.nutrition.zinc;
            dishNutr.iron += ingredient.nutrition.iron;
            dishNutr.manganese += ingredient.nutrition.manganese;
            dishNutr.copper += ingredient.nutrition.copper;
            dishNutr.selenium += ingredient.nutrition.selenium;
            dishNutr.vitaminB1 += ingredient.nutrition.vitaminB1;
            dishNutr.vitaminB2 += ingredient.nutrition.vitaminB2;
            dishNutr.vitaminB3 += ingredient.nutrition.vitaminB3;
            dishNutr.vitaminB5 += ingredient.nutrition.vitaminB5;
            dishNutr.vitaminB6 += ingredient.nutrition.vitaminB6;
            dishNutr.vitaminB7 += ingredient.nutrition.vitaminB7;
            dishNutr.vitaminB9 += ingredient.nutrition.vitaminB9;
            dishNutr.vitaminB12 += ingredient.nutrition.vitaminB12;
            dishNutr.vitaminC += ingredient.nutrition.vitaminC;
            dishNutr.vitaminA += ingredient.nutrition.vitaminA;
            dishNutr.vitaminD += ingredient.nutrition.vitaminD;
            dishNutr.vitaminE += ingredient.nutrition.vitaminE;
            dishNutr.vitaminK += ingredient.nutrition.vitaminK;
        }
    }

}

[System.Serializable]
public class LimiterSet
{
    public Limiter totalEnergyLimit;
    public Limiter carbLimit;
    public Limiter proteinLimit;
    public Limiter fatLimit;
    public Limiter cholesterolLimit;
    public Limiter saturatedFatLimit;
    public Limiter sugarLimit;
    public Limiter fiberLimit;
    public Limiter waterLimit;
    public Limiter PotassiumLimit;
    public Limiter sodiumLimit;
    public Limiter calciumLimit;
    public Limiter phosphorusLimit;
    public Limiter magnesiumLimit;
    public Limiter zincLimit;
    public Limiter ironLimit;
    public Limiter manganeseLimit;
    public Limiter copperLimit;
    public Limiter seleniumLimit;
    public Limiter vitaminALimit;
    public Limiter vitaminB1Limit;
    public Limiter vitaminB2Limit;
    public Limiter vitaminB3Limit;
    public Limiter vitaminB5Limit;
    public Limiter vitaminB6Limit;
    public Limiter vitaminB7Limit;
    public Limiter vitaminB9Limit;
    public Limiter vitaminB12Limit;
    public Limiter vitaminCLimit;
    public Limiter vitaminDLimit;
    public Limiter vitaminELimit;
    public Limiter vitaminKLimit;
}

public class ResultScore
{
    public List<DishScoreHolder> allScore;
    public float finalScore;
    public int finalStar;
    public float totalEnergy;
    //public Gameobject dish;
}

public class DishScoreHolder
{
    public float value;
    public float actualScore;
    public int star;
    public string detail;
    public string comment;
    public Limiter limiter;
    public bool isPriority = false;
}

[System.Serializable]
public class Limiter
{   // -1 = default = null information
    public string name;
    public bool isTop4Priority = false;
    public LimiterType limterType;
    public float median = -1;
    public float lowerLimit = -1;
    public float upperLimit = -1;
    public string unitName;
    public CalculateType calType;
    public float alpha = 0.4f;
    public float beta = 0.2f;
    public float weight;
    public List<string> defectEatTooLittle;
    public List<string> defectOvereating;
    public List<string> commentForGoodDish;
}

[System.Serializable]
public enum LimiterType
{
    None,
    InLimiterBest,
    MoreIsBetter,
    LessIsBetter,
}

[System.Serializable]
public enum CalculateType
{
    None,
    Mass_Gram,
    PercentEnergy,
    BaseTotalEnergy,
    Mass_Miligram,
    Mass_Microgram,
    Mass_Vitamin
}