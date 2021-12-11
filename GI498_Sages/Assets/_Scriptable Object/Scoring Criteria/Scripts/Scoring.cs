using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scorer", menuName = "Scoring Criteria/Scorer")]
public class Scoring : ScriptableObject
{
    [Header("% score Level")]
    public float star5 = 100f/100f;
    public float star4 = 75f/100f;
    public float star3 = 50f/100f;
    public float star2 = 25f/100f;
    public float star1 = 0f;

    [SerializeField] private LevelStandard defaultStandard;

    public EnergyScore energyScore;

    public ResultScore ScoreCalculate(List<IngredientObject> ingredients, LevelStandard starndard)
    {

        var dishNutr = new Nutrition();
        SetZeroNutr(dishNutr);
        SumNitr(dishNutr, ingredients);
        return ValueCalculate(dishNutr, starndard);

        

        //CalculateEachEnergy(carb, protein, etc) => save in DishScoreHolder
        //TotalEnergy = all ENergy => save in DishScoreHolder
        //Calculate other nutrition
        //var BaseTotalEnergy = CalculateScore(dishNutr, starndard.energrScore.totalEnergyLimit, dishScoreHolders);
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

    public ResultScore ValueCalculate(Nutrition dishNutr, LevelStandard standard)
    {
        ResultScore resultScore = new ResultScore();
        List<DishScoreHolder> dishEnergyScore = new List<DishScoreHolder>();
        var changeUnit = 0.001f; // 1 milligram = 0.001 gram

        // 1 kCal = 4.18 kJ
        // 1 kJ = 0.24 kCal

        var carbohydrateEnergy = (dishNutr.carbohydrate + dishNutr.sugars + dishNutr.fiber) * 4 * changeUnit;   // carb 1 g = 4 kCal = 17 kJ
        var proteinEnergy = dishNutr.proteins * 4 * changeUnit;        // prot 1 g =  4 kCal = 17 kJ
        var fatEnergy = (dishNutr.cholesterol + dishNutr.fat) * 9 * changeUnit;   // fat 1 g = 9 kCal = 38 kJ // fat is already include saturatedfat fat
        var alcoholEnergy = 0f;        // alc 1 g = 7 kCal = 29 kJ
        alcoholEnergy *= 5.6f * changeUnit;         // alc 1 mL = 5.6 kCal = 23 kJ        

        var totalEnergy = carbohydrateEnergy + proteinEnergy + fatEnergy + alcoholEnergy;

        DishScoreHolder totalEnergyScore = new DishScoreHolder();
        totalEnergyScore.limiter = standard.energrScore.fatLimit;
        totalEnergyScore.name = "TotalEnergy";
        totalEnergyScore.value = totalEnergy;
        dishEnergyScore.Add(totalEnergyScore);

        //energyScore.carbohydrateProportion = carbohydrateCal / totalEnergy;
        //energyScore.fatProportion = fatCal / totalEnergy;
        //energyScore.proteinProportion = proteinCal / totalEnergy;

        DishScoreHolder carbEnergyScore = new DishScoreHolder();
        carbEnergyScore.limiter = standard.energrScore.carbLimit;
        carbEnergyScore.name = "Carbohydrate";
        carbEnergyScore.value = carbohydrateEnergy;
        dishEnergyScore.Add(carbEnergyScore);

        DishScoreHolder protEnergyScore = new DishScoreHolder();
        protEnergyScore.limiter = standard.energrScore.proteinLimit;
        protEnergyScore.name = "Protein";
        protEnergyScore.value = proteinEnergy;
        dishEnergyScore.Add(protEnergyScore);

        DishScoreHolder fatEnergyScore = new DishScoreHolder();
        fatEnergyScore.limiter = standard.energrScore.fatLimit;
        fatEnergyScore.name = "Fat";
        fatEnergyScore.value = fatEnergy;
        dishEnergyScore.Add(fatEnergyScore);

        DishScoreHolder satFatScore = new DishScoreHolder();
        satFatScore.limiter = standard.energrScore.fiberLimit;
        satFatScore.name = "Fiber";
        satFatScore.value = dishNutr.fiber;
        dishEnergyScore.Add(satFatScore);

        //DishScoreHolder CholScore = new DishScoreHolder();
        //CholScore.limiter = null;
        //CholScore.name = "Cholesterol";
        //CholScore.value = 
        //dishEnergyScore.Add(CholScore);

        //DishScoreHolder alcEnergyScore = new DishScoreHolder();
        //alcEnergyScore.limiter = null;
        //alcEnergyScore.name = "Alcohol";
        //alcEnergyScore.value = alcoholEnergy;
        //dishEnergyScore.Add(alcEnergyScore);

        DishScoreHolder sugarScore = new DishScoreHolder();
        sugarScore.limiter = standard.energrScore.sugarLimit;
        sugarScore.name = "Sugar";
        sugarScore.value = dishNutr.sugars;
        dishEnergyScore.Add(sugarScore);

        DishScoreHolder fiberScore = new DishScoreHolder();
        fiberScore.limiter = standard.energrScore.fiberLimit;
        fiberScore.name = "Fiber";
        fiberScore.value = dishNutr.fiber;
        dishEnergyScore.Add(fiberScore);

        CalculateScore(totalEnergyScore);
        for(int i = 1; i < dishEnergyScore.Count; i++)
        {
            switch(dishEnergyScore[i].limiter.calType)
            {
                case CalculateType.BaseTotalEnergy:
                    CalculateScore(dishEnergyScore[i]);
                    break;
                case CalculateType.PercentEnergy:
                    CalculateScore(dishEnergyScore[i],totalEnergy);
                    break;
                case CalculateType.Mass_Gram:
                    CalculateScore(dishEnergyScore[i]);
                    break;
            }
        }
        return resultScore;
    }

    public void CalculateScore(DishScoreHolder scoreHolder)
    {
        Limiter limiter = scoreHolder.limiter;
        switch (scoreHolder.limiter.limterType)
        {
            case LimiterType.InLimiterBest :
                {
                    if(limiter.lowerLimit != -1 && limiter.upperLimit != -1)
                    {
                        var maxScoreLowerLimit = limiter.lowerLimit + (limiter.upperLimit - limiter.lowerLimit) * ((1 - limiter.alpha) / 2);
                        var maxScoreUpperLimit = limiter.upperLimit - (limiter.upperLimit - limiter.lowerLimit) * ((1 - limiter.alpha) / 2);
                        if (scoreHolder.value >= maxScoreLowerLimit && scoreHolder.value <= maxScoreUpperLimit)
                        {
                            scoreHolder.star = 5;
                            var median = (maxScoreLowerLimit + maxScoreUpperLimit) / 2;
                            if (scoreHolder.value < median)
                            {
                                var lower = maxScoreLowerLimit;
                                scoreHolder.actualScore = star5 + (scoreHolder.value - lower) / (median - lower) * (100f - star5);
                            }
                            else
                            {
                                var upper = maxScoreUpperLimit;
                                scoreHolder.actualScore = star5 + (upper - scoreHolder.value) / (upper - median) * (100f - star5);
                            }
                        }
                        else if(scoreHolder.value >= limiter.lowerLimit && scoreHolder.value <= limiter.upperLimit)
                        {
                            scoreHolder.star = 4;
                            if (scoreHolder.value < maxScoreLowerLimit)
                            {
                                var lower = limiter.lowerLimit;
                                var upper = maxScoreLowerLimit;
                                scoreHolder.actualScore = star4 + (scoreHolder.value - lower) / (upper - lower) * (star5 - star4);
                            }
                            else //scoreHolder.value > maxScoreUpperLimit
                            {
                                var lower = limiter.upperLimit;
                                var upper = maxScoreUpperLimit;
                                scoreHolder.actualScore = star5 + (upper - scoreHolder.value) / (upper - lower) * (star5 - star4);
                            }
                        }
                        else
                        {
                            var betaValue = limiter.lowerLimit * limiter.beta;
                            var zeroStarLowerLimit = limiter.lowerLimit - betaValue;
                            var zeroStarUpperLimit = limiter.upperLimit + betaValue;
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

                                if (scoreHolder.actualScore == 0)
                                {
                                    scoreHolder.star = 0;
                                    Debug.Log("actualScore == 0");
                                }
                                else if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                                else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                                else if (scoreHolder.actualScore >= star1) scoreHolder.star = 1;
                            }
                        }
                    }
                    else if(limiter.median != -1)
                    {

                    }
                    else if(limiter.median == -1 && (limiter.lowerLimit != -1 && limiter.upperLimit != -1))
                    {

                    }
                    break;
                }
            case LimiterType.MoreIsBetter:
                {
                    break;
                }
            case LimiterType.LessIsBetter:
                {
                    break;
                }
            default:
                break;
        }

        if (scoreHolder.actualScore > 100)
            scoreHolder.actualScore = 100;
        else if (scoreHolder.actualScore < 0)
            scoreHolder.actualScore = 0;
    }

    public void CalculateScore(DishScoreHolder scoreHolder, float totalEnergy)
    {
        switch (scoreHolder.limiter.limterType)
        {
            case LimiterType.InLimiterBest:
                {
                    break;
                }
            case LimiterType.MoreIsBetter:
                {
                    break;
                }
            case LimiterType.LessIsBetter:
                {
                    break;
                }
            default:
                break;
        }
    }

    public void CalculateScore(float nutr, Limiter limit, List<DishScoreHolder> dishScoreHolders)
    {
        

        //return null;

    //public string nutrition;
    //public float actualScore;
    //public string detail;
    //public Limiter limiter;

}

    //public Nutrition CalculateLevel(Nutrition dishNutr, Nutrition standard)
    //{
    //    var score = new Nutrition();
    //    score.cholesterol = (standard.cholesterol - dishNutr.cholesterol) / standard.cholesterol;
    //    score.carbohydrate = (standard.carbohydrate - dishNutr.carbohydrate) / standard.carbohydrate;
    //    score.sugars = (standard.sugars - dishNutr.sugars) / standard.sugars;
    //    score.fiber = (standard.fiber - dishNutr.fiber) / standard.fiber;
    //    score.proteins = (standard.proteins - dishNutr.proteins) / standard.proteins;
    //    score.fat = (standard.fat - dishNutr.fat) / standard.fat;
    //    score.saturatedfat = (standard.saturatedfat - dishNutr.saturatedfat) / standard.saturatedfat;
    //    score.water = (standard.water - dishNutr.water) / standard.water;
    //    score.potassium = (standard.potassium - dishNutr.potassium) / standard.potassium;
    //    score.sodium = (standard.sodium - dishNutr.sodium) / standard.sodium;
    //    score.calcium = (standard.calcium - dishNutr.calcium) / standard.calcium;
    //    score.phosphorus = (standard.phosphorus - dishNutr.phosphorus) / standard.phosphorus;
    //    score.magnesium = (standard.magnesium - dishNutr.magnesium) / standard.magnesium;
    //    score.zinc = (standard.zinc - dishNutr.zinc) / standard.zinc;
    //    score.iron = (standard.iron - dishNutr.iron) / standard.iron;
    //    score.manganese = (standard.manganese - dishNutr.manganese) / standard.manganese;
    //    score.copper = (standard.copper - dishNutr.copper) / standard.copper;
    //    score.selenium = (standard.selenium - dishNutr.selenium) / standard.selenium;
    //    score.vitaminB1 = (standard.vitaminB1 - dishNutr.vitaminB1) / standard.vitaminB1;
    //    score.vitaminB2 = (standard.vitaminB2 - dishNutr.vitaminB2) / standard.vitaminB2;
    //    score.vitaminB3 = (standard.vitaminB3 - dishNutr.vitaminB3) / standard.vitaminB3;
    //    score.vitaminB5 = (standard.vitaminB5 - dishNutr.vitaminB5) / standard.vitaminB5;
    //    score.vitaminB6 = (standard.vitaminB6 - dishNutr.vitaminB6) / standard.vitaminB6;
    //    score.vitaminB7 = (standard.vitaminB7 - dishNutr.vitaminB7) / standard.vitaminB7;
    //    score.vitaminB9 = (standard.vitaminB9 - dishNutr.vitaminB9) / standard.vitaminB9;
    //    score.vitaminB12 = (standard.vitaminB12 - dishNutr.vitaminB12) / standard.vitaminB12;
    //    score.vitaminC = (standard.vitaminC - dishNutr.vitaminC) / standard.vitaminC;
    //    score.vitaminA = (standard.vitaminA - dishNutr.vitaminA) / standard.vitaminA;
    //    score.vitaminD = (standard.vitaminD - dishNutr.vitaminD) / standard.vitaminD;
    //    score.vitaminE = (standard.vitaminE - dishNutr.vitaminE) / standard.vitaminE;
    //    score.vitaminK = (standard.vitaminK - dishNutr.vitaminK) / standard.vitaminK;
    //    score.sugars = dishNutr.sugars - standard.sugars;
    //    return score;
    //}

    public void CalculateEachStar(ref float score)
    {
        if (score <= star5) score = 5;
        else if (score <= star4) score = 4;
        else if (score <= star3) score = 3;
        else if (score <= star2) score = 2;
        else if (score <= star1) score = 1;
        else score = 0;
    }

    public void CalculateEachPercent(ref float score, float dishNutr, float standard)
    {
        score = (standard - dishNutr) / standard;
    }

    public void CalNutrToStar(ref float score, float dishNutr, float standard)
    {
        CalculateEachPercent(ref score, dishNutr, standard);
        CalculateEachStar(ref score);
    }

    //public float EnergyCal(Nutrition dishNutr)
    //{
    //    energyScore = new EnergyScore();
    //    var changeUnit = 0.001f; // 1 milligram = 0.001 gram

    //    // 1 kCal = 4.18 kJ
    //    // 1 kJ = 0.24 kCal

    //    var carbohydrateCal = dishNutr.carbohydrate + dishNutr.sugars + dishNutr.fiber;   // carb 1 g = 4 kCal = 17 kJ
    //    carbohydrateCal *= 4 * changeUnit;

    //    var proteinCal = dishNutr.proteins;        // prot 1 g =  4 kCal = 17 kJ
    //    proteinCal *= 4 * changeUnit;

    //    var fatCal = dishNutr.cholesterol + dishNutr.fat;            // fat 1 g = 9 kCal = 38 kJ
    //    fatCal *= 9 * changeUnit;

    //    var alcoholCal = 0f;        // alc 1 g = 7 kCal = 29 kJ
    //    alcoholCal *= 5.6f * changeUnit;         // alc 1 mL = 5.6 kCal = 23 kJ

    //    var totalEnergy = carbohydrateCal + proteinCal + fatCal + alcoholCal;
    //    energyScore.totalEnergy = totalEnergy;
    //    energyScore.carbohydrateProportion = carbohydrateCal / totalEnergy;
    //    energyScore.fatProportion = fatCal / totalEnergy;
    //    energyScore.proteinProportion = proteinCal / totalEnergy;
    //    //energyScore.sugarProportion = dishNutr.sugars * changeUnit * 4 / totalEnergy;
    //    //for sugar : good is <= 5% for TotalEnergy or 6 teaSpoon or 24g/day
    //    //but it only about sugar that we add in dish during cooking. so it's almost nothing with sugar in other ingredient

    //    return energyScore.totalEnergy;
    //}
}

[System.Serializable]
public class EnergyScore
{
    public Limiter totalEnergyLimit; //calories good = 2000 kcal/day => 670 kcal/day
    public Limiter carbLimit; //good = 45-65% for TotalEnergy
    public Limiter proteinLimit; //good = 10-35% for TotalEnergy
    public Limiter fatLimit; //good = 20-35% for TotalEnergy
    public Limiter cholesterolLimit;
    public Limiter saturatedFatLimit;
    public Limiter sugarLimit;
    //good <= 5% for TotalEnergy or 6 teaSpoon or 24g/day
    //but it only about sugar that we add in dish during cooking. so it's almost nothing with sugar in other ingredient
    public Limiter fiberLimit;
    //good => 14g/1000kcal
    //good = 25g - 28g for adult
    //good = age*1 + 5 for child(< 6 years old)
    public Limiter sodiumLimit;
    public Limiter vitaminALimit;
    public Limiter vitaminDLimit;
}

public class ResultScore
{
    public List<DishScoreHolder> allScore;
    public List<DishScoreHolder> fourthPriority;
    public float finalScore;
    public int finalStar;
}

public class DishScoreHolder
{
    public string name;
    public float value;
    public float actualScore;
    public int star;
    public string detail;
    public Limiter limiter;
    //public Gameobject dish;
}

[System.Serializable]
public class Limiter
{   // -1 = default = null information
    public bool isTop4Priority = false;
    public LimiterType limterType;
    public float median = -1;
    public float lowerLimit = -1;
    public float upperLimit = -1;
    public string unitName;
    public CalculateType calType;
    public float alpha = 0.4f;
    public float beta = 0.5f;
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
    BaseTotalEnergy
}