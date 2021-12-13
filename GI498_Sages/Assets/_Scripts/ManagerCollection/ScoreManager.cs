using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.ManagerCollection
{
    public class ScoreManager : MonoBehaviour
    {
        [Serializable]
        public struct ScoreStruct
        {
            public FoodObject foodObject;
            public float foodScore;
        }

        [SerializeField] private List<ScoreStruct> allFoodScoreList = new List<ScoreStruct>();
        [SerializeField] private float totalScore;
        
        //UI Part
        [SerializeField] private Slider starGrade;

        private void Start()
        {
            ClearScoreManager();
        }

        public void CreateScore(FoodObject food,int value)
        {
            var newScore = new ScoreStruct();
            newScore.foodObject = food;
            newScore.foodScore = value;

            allFoodScoreList.Add(newScore);
        }

        public void CalculateTotalScore()
        {
            for (int i = 0; i < allFoodScoreList.Count; i++)
            {
                if (allFoodScoreList[i].foodObject != null)
                {
                    totalScore += allFoodScoreList[i].foodScore;
                }
            }

            // Total score = Total score / all food
            totalScore = totalScore / allFoodScoreList.Count;
            CalculateStarGrade();
        }

        public void CalculateStarGrade()
        {
            // Star = TotalScore / Max of 5 stars
            starGrade.value = totalScore / 5;
            
            // Clear
            ClearScoreManager();
        }
        
        public void ClearScoreManager()
        {
            totalScore = 0;

            if (allFoodScoreList.Count > 0)
            {
                allFoodScoreList.Clear();
            }
        }
    }
}