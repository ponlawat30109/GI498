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
            public GameObject particlePrefab;
            public StatusEnum status;
        }
        
        [SerializeField] private Image statusImage;
        [SerializeField] private GameObject currentEffect;
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
                    statusImage.sprite = GetStatus(StatusEnum.Finish).sprite;
                    currentEffect = GetStatus(StatusEnum.Finish).particlePrefab;
                    break;
                }
                case StatusEnum.Cooking:
                {
                    statusImage.sprite = GetStatus(StatusEnum.Cooking).sprite;
                    currentEffect = GetStatus(StatusEnum.Cooking).particlePrefab;
                    break;
                }
                case StatusEnum.Wait:
                {
                    statusImage.sprite = GetStatus(StatusEnum.Wait).sprite;
                    currentEffect = GetStatus(StatusEnum.Wait).particlePrefab;
                    break;
                }
                
                case StatusEnum.Fail:
                {
                    statusImage.sprite = GetStatus(StatusEnum.Fail).sprite;
                    currentEffect = GetStatus(StatusEnum.Fail).particlePrefab;
                    break;
                }
                case StatusEnum.Trash:
                {
                    statusImage.sprite = GetStatus(StatusEnum.Trash).sprite;
                    currentEffect = GetStatus(StatusEnum.Trash).particlePrefab;
                    break;
                }
            }
        }

        private Status GetStatus(StatusEnum target)
        {
            return statusList.Find(x=> x.status == target);
        }
        
        /*private Sprite GetSpriteStatus(StatusEnum target)
        {
            foreach (var status in statusList)
            {
                if (status.status == target)
                {
                    return status.sprite;
                }
            }

            return null;
        }*/
    }
}