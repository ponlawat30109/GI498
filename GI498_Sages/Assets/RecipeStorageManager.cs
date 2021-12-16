using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.NPCSctipts;
using UnityEngine;

public class RecipeStorageManager : MonoBehaviour
{
    [Serializable]
    public struct RecipeColleciton
    {
        public List<FoodObject> recipeList;
        public NpcInformation.NpcPatientType type;
    }

    [SerializeField] private List<RecipeColleciton> recipeCollecitons;


    public RecipeColleciton GetRecipeCollectionByType(NpcInformation.NpcPatientType typeToGet)
    {
        var rObj = new RecipeColleciton();
        
        foreach (var colleciton in recipeCollecitons)
        {
            if (colleciton.type == typeToGet)
            {
                rObj = colleciton;
            }
        }

        if (rObj.recipeList.Count < 1)
        {
            Debug.Log("[RecipeStorageManager.cs] Try to get null collection.");
        }
        
        return rObj;
    }
}
