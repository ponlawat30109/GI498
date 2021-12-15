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
        var fatEnergy = (dishNutr.cholesterol + dishNutr.fat) * 9 * changeUnit;   // fat 1 g = 9 kCal = 38 kJ // fat is already include saturatedfat fat
        var alcoholEnergy = 0f;        // alc 1 g = 7 kCal = 29 kJ
        alcoholEnergy *= 5.6f * changeUnit;         // alc 1 mL = 5.6 kCal = 23 kJ        

        var totalEnergy = carbohydrateEnergy + proteinEnergy + fatEnergy + alcoholEnergy;

        if (standard.limiterSet.totalEnergyLimit.weight > 0)
        {
            DishScoreHolder totalEnergyScore = new DishScoreHolder();
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
                carbEnergyScore.value = carbohydrateEnergy;
                allScore.Add(carbEnergyScore);
            }
        }

        if (standard.limiterSet.proteinLimit.weight > 0)
        {
            DishScoreHolder protEnergyScore = new DishScoreHolder();
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
                protEnergyScore.value = proteinEnergy;
                allScore.Add(protEnergyScore);
            }
        }

        if (standard.limiterSet.fatLimit.weight > 0)
        {
            DishScoreHolder fatEnergyScore = new DishScoreHolder();
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
                fatEnergyScore.value = fatEnergy;
                allScore.Add(fatEnergyScore);
            }
        }

        if (standard.limiterSet.saturatedFatLimit.weight > 0)
        {
            DishScoreHolder satFatScore = new DishScoreHolder();
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
                satFatScore.value = dishNutr.saturatedfat;
                allScore.Add(satFatScore);
            }
        }

        if (standard.limiterSet.sugarLimit.weight > 0)
        {
            DishScoreHolder sugarScore = new DishScoreHolder();
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
                sugarScore.value = dishNutr.sugars;
                allScore.Add(sugarScore);
            }
        }

        if (standard.limiterSet.fiberLimit.weight > 0)
        {
            DishScoreHolder fiberScore = new DishScoreHolder();
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

        if (standard.limiterSet.fiberLimit.weight > 0)
        {
            DishScoreHolder fiberScore = new DishScoreHolder();
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
            case CalculateType.Mass_Miligram:
                {
                    //do not thing
                    break;
                }
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
                        if (scoreHolder.value < limiter.lowerLimit * totalEnergy)
                        {
                            scoreHolder.detail = "< " + limiter.lowerLimit * totalEnergy;
                        }
                        else if (scoreHolder.value > limiter.upperLimit * totalEnergy)
                        {
                            scoreHolder.detail = "> " + limiter.upperLimit * totalEnergy;
                        }
                        else
                        {
                            scoreHolder.detail = $"= {limiter.lowerLimit * totalEnergy} ~ {limiter.upperLimit * totalEnergy}";
                        }
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
                        if (scoreHolder.value < limiter.median * totalEnergy)
                        {
                            scoreHolder.detail = $"Good Value: {limiter.median * totalEnergy} (<)";
                        }
                        else if (scoreHolder.value > limiter.median * totalEnergy)
                        {
                            scoreHolder.detail = $"Good Value: {limiter.median * totalEnergy} (>)";
                        }
                        else
                        {
                            scoreHolder.detail = $"Good Value: {limiter.median * totalEnergy} (=)";
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
                        if (scoreHolder.value < limiter.lowerLimit * totalEnergy)
                        {
                            scoreHolder.detail = "< " + limiter.lowerLimit * totalEnergy;
                        }
                        else if (scoreHolder.value > limiter.lowerLimit * totalEnergy)
                        {
                            scoreHolder.detail = "> " + limiter.lowerLimit * totalEnergy;
                        }
                        else
                        {
                            scoreHolder.detail = $"= {limiter.lowerLimit * totalEnergy}";
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
                        if (scoreHolder.value < limiter.upperLimit * totalEnergy)
                        {
                            scoreHolder.detail = "< " + limiter.upperLimit * totalEnergy;
                        }
                        else if (scoreHolder.value > limiter.upperLimit * totalEnergy)
                        {
                            scoreHolder.detail = "> " + limiter.upperLimit * totalEnergy;
                        }
                        else
                        {
                            scoreHolder.detail = $"= {limiter.upperLimit * totalEnergy}";
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
    public Limiter waterLimit;
    public Limiter vitaminALimit;
    public Limiter vitaminDLimit;
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
    Mass_Miligram
}