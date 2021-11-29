using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.CookingSystem.UI
{
    public class StoveStatusUI : MonoBehaviour
    {
        public enum StatusEnum
        {
            Finish,
            Cooking,
            Wait,
            Fail,
            Trash
        }

        [Serializable]
        public struct Status
        {
            public Sprite sprite;
            public StatusEnum status;
        }
        
        [SerializeField] private Image statusImage;
        [SerializeField] private StatusEnum currentStatus;
        [SerializeField] private List<Status> statusList = new List<Status>();

        public void SetCurrentStatus(StatusEnum statusEnum)
        {
            currentStatus = statusEnum;
            SetStatusImageByStatusEnum();
        }
        
        public void SetStatusImageByStatusEnum()
        {
            switch (currentStatus)
            {
                case StatusEnum.Finish:
                {
                    statusImage.sprite = GetSpriteStatus(StatusEnum.Finish);
                    break;
                }
                case StatusEnum.Cooking:
                {
                    statusImage.sprite = GetSpriteStatus(StatusEnum.Cooking);
                    break;
                }
                case StatusEnum.Wait:
                {
                    statusImage.sprite = GetSpriteStatus(StatusEnum.Wait);
                    break;
                }
                
                case StatusEnum.Fail:
                {
                    statusImage.sprite = GetSpriteStatus(StatusEnum.Fail);
                    break;
                }
                case StatusEnum.Trash:
                {
                    statusImage.sprite = GetSpriteStatus(StatusEnum.Trash);
                    break;
                }
            }
        }

        private Sprite GetSpriteStatus(StatusEnum target)
        {
            foreach (var status in statusList)
            {
                if (status.status == target)
                {
                    return status.sprite;
                }
            }

            return null;
        }
    }
}