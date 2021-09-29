using System;
using UnityEngine;

namespace _Scriptable_Object.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Tool Object", menuName = "Inventory System/Items/Tool")]
    public class ToolObject : ItemObject
    {
        // Implement Later..
        private void Awake()
        {
            type = ItemType.Tool;
        }
    }
}