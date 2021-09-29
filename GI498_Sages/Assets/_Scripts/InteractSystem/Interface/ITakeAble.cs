using _Scripts.InventorySystem;

namespace _Scripts.InteractSystem.Interface
{
    public interface ITakeAble
    {
        void TakeIn(ContainerObject newContainer, ContainerObject oldContainer, ItemObject takeInItem);
        void TakeOut(ContainerObject newContainer, ContainerObject oldContainer, ItemObject takeOutItem);
    }
}