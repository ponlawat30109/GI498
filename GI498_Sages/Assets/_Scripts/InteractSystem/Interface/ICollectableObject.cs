using System;
using _Scripts.InventorySystem.ScriptableObjects.Storage;

namespace _Scripts.InteractSystem.Interface
{
    public interface ICollectableObject
    {
        event Action OnCollected;
        void CollectTo(StorageObject whereToPutIn);
    }
}