// #if UNITY_EDITOR

// using UnityEditor;

// #endif
using UnityEngine;

public enum ItemType
{
    Ingredient,
    Tool,
    Food,
    Default
}

public class ItemObject : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType type;
    public GameObject ingamePrefab;
    [TextArea(15, 20)] 
    public string description;
    
}