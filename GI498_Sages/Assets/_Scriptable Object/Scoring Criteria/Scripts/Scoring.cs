using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scorer", menuName = "Scoring Criteria/Scorer")]
public class Scoring : ScriptableObject
{
    [Header("% score Level")]
    public float star5 = 100f / 100f;
    public float star4 = 75f / 100f;
    public float star3 = 50f / 100f;
    public float star2 = 25f / 100f;
    public float star1 = 0f;

    [SerializeField] private LevelStandard defaultStandard;


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
        List<DishScoreHolder> allScore = new List<DishScoreHolder>();
        List<DishScoreHolder> fourthPriority = new List<DishScoreHolder>();
        var changeUnit = 0.001f; // 1 milligram = 0.001 gram

        // 1 kCal = 4.18 kJ
        // 1 kJ = 0.24 kCal

        //var carbohydrateEnergy = (dishNutr.carbohydrate + dishNutr.sugars + dishNutr.fiber) * 4 * changeUnit;   // carb 1 g = 4 kCal = 17 kJ
        var carbohydrateEnergy = dishNutr.carbohydrate * 4 * changeUnit;
        var proteinEnergy = dishNutr.proteins * 4 * changeUnit;        // prot 1 g =  4 kCal = 17 kJ
        var fatEnergy = (dishNutr.cholesterol + dishNutr.fat) * 9 * changeUnit;   // fat 1 g = 9 kCal = 38 kJ // fat is already include saturatedfat fat
        var alcoholEnergy = 0f;        // alc 1 g = 7 kCal = 29 kJ
        alcoholEnergy *= 5.6f * changeUnit;         // alc 1 mL = 5.6 kCal = 23 kJ        

        var totalEnergy = carbohydrateEnergy + proteinEnergy + fatEnergy + alcoholEnergy;

        DishScoreHolder totalEnergyScore = new DishScoreHolder();
        if(standard.energrScore.totalEnergyLimit.limterType != LimiterType.None)
            totalEnergyScore.limiter = standard.energrScore.totalEnergyLimit;
        else if (defaultStandard != null)
        {
            if (defaultStandard.energrScore.totalEnergyLimit.limterType != LimiterType.None)
            {
                totalEnergyScore.limiter = defaultStandard.energrScore.totalEnergyLimit;
            }
        }
        if (totalEnergyScore.limiter != null)
        {
            totalEnergyScore.value = totalEnergy;
            allScore.Add(totalEnergyScore);
        }

        //energyScore.carbohydrateProportion = carbohydrateCal / totalEnergy;
        //energyScore.fatProportion = fatCal / totalEnergy;
        //energyScore.proteinProportion = proteinCal / totalEnergy;

        DishScoreHolder carbEnergyScore = new DishScoreHolder();
        if (standard.energrScore.carbLimit.limterType != LimiterType.None)
            carbEnergyScore.limiter = standard.energrScore.carbLimit;
        else if (defaultStandard != null)
        {
            if (defaultStandard.energrScore.carbLimit.limterType != LimiterType.None)
            {
                carbEnergyScore.limiter = defaultStandard.energrScore.carbLimit;
            }
        }
        if (carbEnergyScore.limiter != null)
        {
            carbEnergyScore.value = carbohydrateEnergy;
            allScore.Add(carbEnergyScore);
        }

        DishScoreHolder protEnergyScore = new DishScoreHolder();
        if (standard.energrScore.proteinLimit.limterType != LimiterType.None)
            protEnergyScore.limiter = standard.energrScore.proteinLimit;
        else if (defaultStandard != null)
        {
            if (defaultStandard.energrScore.proteinLimit.limterType != LimiterType.None)
            {
                protEnergyScore.limiter = defaultStandard.energrScore.proteinLimit;
            }
        }
        if (protEnergyScore.limiter != null)
        {
            protEnergyScore.value = proteinEnergy;
            allScore.Add(protEnergyScore);
        }
        
        DishScoreHolder fatEnergyScore = new DishScoreHolder();
        if (standard.energrScore.fatLimit.limterType != LimiterType.None)
            fatEnergyScore.limiter = standard.energrScore.fatLimit;
        else if (defaultStandard != null)
        {
            if (defaultStandard.energrScore.fatLimit.limterType != LimiterType.None)
            {
                fatEnergyScore.limiter = defaultStandard.energrScore.fatLimit;
            }
        }
        if (fatEnergyScore.limiter != null)
        {
            fatEnergyScore.value = fatEnergy;
            allScore.Add(fatEnergyScore);
        }
        

        DishScoreHolder satFatScore = new DishScoreHolder();
        if (standard.energrScore.saturatedFatLimit.limterType != LimiterType.None)
            satFatScore.limiter = standard.energrScore.saturatedFatLimit;
        else if (defaultStandard != null)
        {
            if (defaultStandard.energrScore.saturatedFatLimit.limterType != LimiterType.None)
            {
                satFatScore.limiter = defaultStandard.energrScore.saturatedFatLimit;
            }
        }
        if (satFatScore.limiter != null)
        {
            satFatScore.value = dishNutr.saturatedfat;
            allScore.Add(satFatScore);
        }
        

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
        if (standard.energrScore.sugarLimit.limterType != LimiterType.None)
            sugarScore.limiter = standard.energrScore.sugarLimit;
        else if (defaultStandard != null)
        {
            if (defaultStandard.energrScore.sugarLimit.limterType != LimiterType.None)
            {
                sugarScore.limiter = defaultStandard.energrScore.sugarLimit;
            }
        }
        if (sugarScore.limiter != null)
        {
            sugarScore.value = dishNutr.sugars;
            allScore.Add(sugarScore);
        }
        
        DishScoreHolder fiberScore = new DishScoreHolder();
        if (standard.energrScore.fiberLimit.limterType != LimiterType.None)
            fiberScore.limiter = standard.energrScore.fiberLimit;
        else if (defaultStandard != null)
        {
            if (defaultStandard.energrScore.fiberLimit.limterType != LimiterType.None)
            {
                fiberScore.limiter = defaultStandard.energrScore.fiberLimit;
            }
        }
        if (fiberScore.limiter != null)
        {
            fiberScore.value = dishNutr.fiber;
            allScore.Add(fiberScore);
        }

        CalculateScore(totalEnergyScore);
        foreach(var scoreHolder in allScore)
        {
            if(scoreHolder.limiter.weight > 0)
            {
                if (scoreHolder.limiter.isTop4Priority)
                    fourthPriority.Add(scoreHolder);

                switch(scoreHolder.limiter.calType)
                {
                    case CalculateType.BaseTotalEnergy:
                        CalculateScore(scoreHolder);
                        break;
                    case CalculateType.PercentEnergy:
                        CalculateScore(scoreHolder, totalEnergy);
                        break;
                    case CalculateType.Mass_Gram:
                        CalculateScore(scoreHolder);
                        break;
                }
            }
        }

        CalculateFinalScore(resultScore);
        
        return resultScore;
    }

    public void CalculateFinalScore(ResultScore resultScoreHolder)
    {
        float totalWeight = 0;
        float totalScore = 0;
        foreach(var scoreHolder in resultScoreHolder.allScore)
        {
            if(scoreHolder.limiter.weight > 0)
            {
                totalWeight += scoreHolder.limiter.weight;
                totalScore += scoreHolder.limiter.weight * scoreHolder.actualScore;
            }
        }

        var finalScore = totalScore / totalWeight;
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
        switch (scoreHolder.limiter.limterType)
        {
            case LimiterType.InLimiterBest:
                {
                    if (limiter.lowerLimit != -1 && limiter.upperLimit != -1)
                    {
                        var fiveStarLowerLimit = limiter.lowerLimit + (limiter.upperLimit - limiter.lowerLimit) * ((1f - limiter.alpha) / 2);
                        var fiveStarUpperLimit = limiter.upperLimit - (limiter.upperLimit - limiter.lowerLimit) * ((1f - limiter.alpha) / 2);
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
                    }
                    else if (limiter.median != -1)
                    {
                        var alphaValue = limiter.median * ((1f - limiter.alpha) / 2);
                        var fiveStarLowerLimit = limiter.median - alphaValue;
                        var fiveStarUpperLimit = limiter.median - alphaValue;
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
                                    scoreHolder.actualScore = (scoreHolder.value - lower) / (upper - lower) * (star5 - star1);
                                }
                                else if (scoreHolder.value > limiter.upperLimit)
                                {
                                    var lower = limiter.upperLimit;
                                    var upper = zeroStarUpperLimit;
                                    scoreHolder.actualScore = (upper - scoreHolder.value) / (upper - lower) * (star5 - star1);
                                }

                                if (scoreHolder.actualScore >= star4) scoreHolder.star = 4;
                                else if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                                else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                                else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                                else scoreHolder.star = 0;
                            }
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
                        var fiveStarLowerLimit = limiter.lowerLimit + ((limiter.lowerLimit - zeroStarLowerLimit) / (1 - limiter.alpha) * limiter.alpha);
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
                    }
                    break;
                }
            case LimiterType.LessIsBetter:
                {
                    if (limiter.upperLimit != -1)
                    {
                        var fiveStarUpperLimit = limiter.upperLimit * (1 - limiter.alpha);
                        var zeroStarUpperLimit = limiter.upperLimit / (1 - limiter.beta) * limiter.beta;
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
                    }
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
        //Calculate by PercentEnergy Concept
        Limiter limiter = scoreHolder.limiter;
        var percentEnergy = scoreHolder.value / totalEnergy;
        switch (scoreHolder.limiter.limterType)
        {
            case LimiterType.InLimiterBest:
                {
                    if (limiter.lowerLimit != -1 && limiter.upperLimit != -1)
                    {
                        var fiveStarLowerLimit = limiter.lowerLimit + (limiter.upperLimit - limiter.lowerLimit) * ((1f - limiter.alpha) / 2);
                        var fiveStarUpperLimit = limiter.upperLimit - (limiter.upperLimit - limiter.lowerLimit) * ((1f - limiter.alpha) / 2);
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
                    }
                    else if (limiter.median != -1)
                    {
                        var alphaValue = limiter.median * ((1f - limiter.alpha) / 2);
                        var fiveStarLowerLimit = limiter.median - alphaValue;
                        var fiveStarUpperLimit = limiter.median - alphaValue;
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
                            var zeroStarLowerLimit = limiter.lowerLimit - betaValue;
                            var zeroStarUpperLimit = limiter.upperLimit + betaValue;
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
                                    scoreHolder.actualScore = (percentEnergy - lower) / (upper - lower) * (star5 - star1);
                                }
                                else if (percentEnergy > limiter.upperLimit)
                                {
                                    var lower = limiter.upperLimit;
                                    var upper = zeroStarUpperLimit;
                                    scoreHolder.actualScore = (upper - percentEnergy) / (upper - lower) * (star5 - star1);
                                }

                                if (scoreHolder.actualScore >= star4) scoreHolder.star = 4;
                                else if (scoreHolder.actualScore >= star3) scoreHolder.star = 3;
                                else if (scoreHolder.actualScore >= star2) scoreHolder.star = 2;
                                else if (scoreHolder.actualScore > star1) scoreHolder.star = 1;
                                else scoreHolder.star = 0;
                            }
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
                        var fiveStarLowerLimit = limiter.lowerLimit + ((limiter.lowerLimit - zeroStarLowerLimit) / (1 - limiter.alpha) * limiter.alpha);
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
                    }
                    break;
                }
            case LimiterType.LessIsBetter:
                {
                    if (limiter.upperLimit != -1)
                    {
                        var fiveStarUpperLimit = limiter.upperLimit * (1 - limiter.alpha);
                        var zeroStarUpperLimit = limiter.upperLimit / (1 - limiter.beta) * limiter.beta;
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
                    }
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
    BaseTotalEnergy
}