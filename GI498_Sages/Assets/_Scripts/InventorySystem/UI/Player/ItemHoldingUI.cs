using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.UI.Player
{
    public class ItemHoldingUI : MonoBehaviour
    {
        [SerializeField] private ItemObject item;
        [SerializeField] private Image itemImage;
    
        [SerializeField] private GameObject parentStorage;

        public void InitializeItem(ItemObject toInitItem,GameObject _parentStorage)
        {
            item = toInitItem;
            itemImage.sprite = toInitItem.itemIcon;
            parentStorage = _parentStorage;
            transform.SetParent(parentStorage.transform);
        }
    }
}
