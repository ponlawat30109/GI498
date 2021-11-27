using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rank
{
    public string rankName;
    public Sprite sprite;
    public int minExperience; //Minimum Exp to be in this rank
    //public Rank nextRank;
    public FoodObject newRecipe;
    public IngredientObject newIngredient;
}
