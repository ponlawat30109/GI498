using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.NotifySystem
{
    public class NotifyComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private float removeTime = 3;
        [SerializeField] private bool isInit;
        [SerializeField] private Button closeButton;

        [SerializeField] private float currentTime = 0;
        
        private void Start()
        {
            isInit = false;
            currentTime = 0;
            closeButton.onClick.AddListener(DestroyNotify);
        }

        private void Update()
        {
            if (IsInit())
            {
                if (currentTime < removeTime)
                {
                    currentTime += Time.deltaTime;
                }
                else if(currentTime >= removeTime)
                {
                    DestroyNotify();
                }
            }
        }

        public void InitNotifyComponent(string title, string message)
        {
            titleText.text = title;
            messageText.text = message;
            
            isInit = true;
        }

        public bool IsInit()
        {
            if (titleText.text != null || titleText.text != "" && messageText.text != null || messageText.text != "")
            {
                isInit = true;
            }
            else
            {
                isInit = false;
            }

            return isInit;
        }
        
        private void DestroyNotify()
        {
            Destroy(this.gameObject);
        }
    }
}
