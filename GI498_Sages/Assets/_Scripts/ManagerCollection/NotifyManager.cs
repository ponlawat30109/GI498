using System.Collections.Generic;
using _Scripts.NotifySystem;
using UnityEngine;

namespace _Scripts.ManagerCollection
{
    public class NotifyManager : MonoBehaviour
    {
        [SerializeField] private Transform notifyContainer;
        [SerializeField] private GameObject componentPrefab;

        public void CreateNotify(string title, string message)
        {
            var newObj = Instantiate(componentPrefab, notifyContainer);
            var newNotify = newObj.GetComponent<NotifyComponent>();
            newNotify.InitNotifyComponent(title, message);
        }
    }
}