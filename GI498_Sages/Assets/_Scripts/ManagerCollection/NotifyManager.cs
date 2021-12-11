using System;
using System.Collections.Generic;
using _Scripts.NotifySystem;
using UnityEngine;

namespace _Scripts.ManagerCollection
{
    public class NotifyManager : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        [SerializeField] private Transform notifyContainer;
        [SerializeField] private GameObject componentPrefab;

        [SerializeField] private List<NotifyComponent> notifyList = new List<NotifyComponent>();
        
        
        

        private void Update()
        {
            if (notifyList.Count > 0)
            {
                parent.gameObject.SetActive(true);
            }
            else
            {
                parent.gameObject.SetActive(false);
            }

            if (notifyList.Count > 0)
            {
                for (int i = 0; i < notifyList.Count; i++)
                {
                    if (notifyList[i] == null)
                    {
                        notifyList.Clear();
                    }
                }
            }
        }

        public void CreateNotify(string title, string message)
        {
            var newObj = Instantiate(componentPrefab, notifyContainer);
            var newNotify = newObj.GetComponent<NotifyComponent>();
            newNotify.InitNotifyComponent(title, message);
                
            notifyList.Add(newNotify);
        }
    }
}