using _Scripts.InventorySystem.ScriptableObjects.Storage;

namespace _Scripts.InventorySystem.Interface
{
    public interface ITakeable
    {
        /// <summary>
        /// Put Item Into A storage from B storage
        /// </summary>
        /// <param name="a">Storage to Add Item</param>
        /// <param name="b">Storage to Remove Item</param>
        /// <param name="item"></param>
        public bool PutIn(StorageObject a, StorageObject b, ItemObject item);

        /// <summary>
        /// Take Out Item from A storage and Add to B storage
        /// </summary>
        /// <param name="a">Storage to Take(Remove) Item</param>
        /// <param name="b">Storage to Add Item</param>
        /// <param name="item"></param>
        public bool TakeOut(StorageObject a, StorageObject b, ItemObject item);
    }
}