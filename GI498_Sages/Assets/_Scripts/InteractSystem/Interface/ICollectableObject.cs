using System;

namespace _Scripts.InteractSystem.Interface
{
    public interface ICollectableObject
    {
        event Action OnCollected;
        void CollectTo(ContainerObject whereToPutIn);
    }
}