using System;

namespace _Scripts.Interact_System.Interface
{
    public interface IInteractableObject
    {
        event Action OnInteracted;
        void Interacted();
    }
}