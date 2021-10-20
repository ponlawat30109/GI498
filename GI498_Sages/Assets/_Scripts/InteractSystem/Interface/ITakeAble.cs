using _Scripts.InventorySystem;
using _Scripts.InventorySystem.ScriptableObjects.Storage;

namespace _Scripts.InteractSystem.Interface
{
    public interface ITakeAble
    {
        void TakeIn(StorageObject newContainer, StorageObject oldContainer, ItemObject takeInItem);
        void TakeOut(StorageObject newContainer, StorageObject oldContainer, ItemObject takeOutItem);
    }
}