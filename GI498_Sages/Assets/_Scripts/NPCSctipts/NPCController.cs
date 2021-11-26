using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCScript
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private PlayerAnimController animCtrl;
        public ModelScript.ModelComponent modelCom;
        [SerializeField] private float speed;
        [SerializeField] private float angularSpeed;
        private StandPoint targetPoint;
        private StandPoint nextTargetPoint;
        [SerializeField] private NPCState npcState;

        public bool isPause = false;

        private enum NPCState
        {
            Walk,
            Order,
            WaitOrder
        }

        private void Start()
        {
            npcState = NPCState.Walk;
        }

        private void Update()
        {
            if(targetPoint == null)
                Debug.Log("targetPoint false");

            switch(npcState)
            {
                case NPCState.Walk:
                    {
                        if (targetPoint.isAvailable)
                        {
                            animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Walk);
                            Walk(targetPoint.transform.position);
                            if (transform.position == targetPoint.transform.position)
                            {
                                if (targetPoint.isOrderPoint)
                                {
                                    npcState = NPCState.Order;
                                    animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Stand);
                                }
                                else
                                {
                                    NextPoint();
                                }
                            }
                        }
                        break;
                    }
                case NPCState.Order:
                    {
                        //if Order == null
                        //Order();
                        FaceTarget(targetPoint.transform.rotation);
                        var faceAngle = Mathf.Abs(Quaternion.Angle(targetPoint.transform.rotation, transform.rotation));
                        if (faceAngle < 3f)
                        {
                            npcState = NPCState.WaitOrder;
                        }
                        break;
                    }
                case NPCState.WaitOrder:
                    {

                        break;
                    }
                default:
                    {
                        npcState = NPCState.Walk;
                        break;
                    }
            }

            //if (!isPause && targetPoint != null)
            //{   //go to next point
            //    if (targetPoint.isAvailable) //true
            //    {
            //        if (transform.position != targetPoint.transform.position)
            //        {
            //            animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Walk);
            //            Walk(targetPoint.transform.position);
            //        }
            //        else if(transform.rotation != targetPoint.transform.rotation)
            //        {
            //            animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Stand);
            //            FaceTarget(targetPoint.transform.rotation);
            //        }
            //        else
            //        {
            //            targetPoint.npcOwner = null;
            //            targetPoint = nextTargetPoint;
            //        }
            //    }
            //    else
            //    {
            //        animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Stand);
            //    }
            //}
        }

        private void Walk(Vector3 targetPosition)
        {
            FaceTarget(targetPosition);
            //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
            var distance = Vector3.Distance(transform.position, targetPosition);
            if (distance < speed * Time.deltaTime)
                transform.position = targetPosition;
            else
            {
                var direction = (targetPosition - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.position += direction * speed * Time.deltaTime;
            }
        }

        private void FaceTarget(Vector3 targetPosition)
        {
            var direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angularSpeed);
            
        }

        private void FaceTarget(Quaternion targetRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * angularSpeed);
        }

        public void GetInitTarget(StandPoint point)
        {
            if (targetPoint == null)
            {
                targetPoint = point;
                targetPoint.npcOwner = this;
                nextTargetPoint = targetPoint.nextPoint;
            }
            else
            {
                nextTargetPoint = point;
            }
        }

        public void GetOrder()
        {
            if(npcState == NPCState.Order)
            {
                NextPoint();
                npcState = NPCState.Walk;
            }
        }

        private void NextPoint()
        {
            targetPoint.npcOwner = null;
            targetPoint = targetPoint.nextPoint;
        }
    }
}
