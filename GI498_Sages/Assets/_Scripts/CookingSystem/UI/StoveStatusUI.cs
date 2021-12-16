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
        
        [Serializable]
        public struct EffectObject
        {
            public GameObject obj;
            public StatusEnum status;
        }
        
        [SerializeField] private Image statusImage;
        [SerializeField] private Image statusBlackImage;
        [SerializeField] private StatusEnum currentStatus;
        [SerializeField] private List<Status> statusList = new List<Status>();
        [SerializeField] private List<EffectObject> effectList = new List<EffectObject>();

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
                    var s = GetStatus(StatusEnum.Finish);
                    statusImage.sprite = s.sprite;
                    statusBlackImage.sprite = s.sprite;
                    SetActiveEffectByStatus(s.status);
                    
                    break;
                }
                case StatusEnum.Cooking:
                {
                    var s = GetStatus(StatusEnum.Cooking);
                    statusImage.sprite = s.sprite;
                    statusBlackImage.sprite = s.sprite;
                    SetActiveEffectByStatus(s.status);

                    break;
                }
                case StatusEnum.Wait:
                {
                    var s = GetStatus(StatusEnum.Wait);
                    statusImage.sprite = s.sprite;
                    statusBlackImage.sprite = s.sprite;
                    SetActiveEffectByStatus(s.status);

                    break;
                }
                
                case StatusEnum.Fail:
                {
                    var s = GetStatus(StatusEnum.Fail);
                    statusImage.sprite = s.sprite;
                    statusBlackImage.sprite = s.sprite;
                    SetActiveEffectByStatus(s.status);

                    break;
                }
                case StatusEnum.Trash:
                {
                    var s = GetStatus(StatusEnum.Trash);
                    statusImage.sprite = s.sprite;
                    statusBlackImage.sprite = s.sprite;
                    SetActiveEffectByStatus(s.status);

                    break;
                }
            }
        }

        private Status GetStatus(StatusEnum target)
        {
            return statusList.Find(x=> x.status == target);
        }

        private void SetActiveEffectByStatus(StatusEnum target)
        {
            foreach (var fx in effectList)
            {
                if (fx.status == target)
                {
                    fx.obj.SetActive(true);
                }
                else
                {
                    fx.obj.SetActive(false);
                }
            }
        }
    }
}