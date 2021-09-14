using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Nutrition
{
    [Header("Name                             Miligrams")]
    public float cholesterol;
    public float sugars;
    public float fiber;
    public float proteins;
    public float fat;
    public float saturatedfat;
    public float water;
    public float potassium;
    public float sodium;
    public float calcium;
    public float phosphorus;
    public float magnesium;
    public float zinc;
    public float iron;
    public float manganese;
    public float copper;
    public float selenium;
    public float vitaminB1;
    public float vitaminB2;
    public float vitaminB3;
    public float vitaminB5;
    public float vitaminB6;
    public float vitaminB7;
    public float vitaminB9;
    public float vitaminB12;
    public float vitaminC;
    public float vitaminA;
    public float vitaminD;
    public float vitaminE;
    public float vitaminK;
}

[CreateAssetMenu(fileName = "New Ingredient Object", menuName = "Inventory System/Items/Ingredient")]
public class IngredientObject : ItemObject
{
    public Nutrition nutrition;

    private void Awake()
    {
        type = ItemType.Ingredient;
    }
}
