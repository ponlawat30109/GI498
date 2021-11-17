using System;

namespace _Scripts.InteractSystem.Interface
{
    public interface IInteractableObject
    {
        event Action OnInteracted;
        void Interacted();
    }
}