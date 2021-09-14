using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Ingredient,
    Tool,
    Food,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject defaultPrefab;
    public ItemType type;
    [TextArea(15, 20)] 
    public string description;
    
}