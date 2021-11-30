using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCScript
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private PlayerAnimController animCtrl;
        [SerializeField] private ModelScript.ModelComponent modelCom;
        [SerializeField] private float speed;
        [SerializeField] private float angularSpeed;
        private StandPoint targetPoint;
        [SerializeField] private NPCState npcState;

        private static int numberOfNpcInQueue = 0;

        public bool isPause = false;
        public bool OnOrder;

        private enum NPCState
        {
            Walk,
            Order,
            WaitOrder,
            Idle //for not show in queue
        }

        private void Start()
        {
            OnOrder = false;
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
                                    NPCManager.Instance.RandomFood(this);
                                }
                                else if(targetPoint.isReleasePoint)
                                {
                                    npcState = NPCState.Idle;
                                    animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Stand);
                                    NPCManager.Instance.SetWaitingNpc(this);
                                    modelCom.RandomSkin();
                                }
                                else
                                {
                                    NextPoint();
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("TargetPoint: " + targetPoint.name);
                        }
                        break;
                    }
                case NPCState.Order:
                    {
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
                        //if get order
                        //see >> GetOrder()

                        break;
                    }
                case NPCState.Idle:
                    {
                        break;
                    }
                default:
                    {
                        npcState = NPCState.Walk;
                        break;
                    }
            }
        }

        private void Walk(Vector3 targetPosition)
        {
            var faceAngle = Mathf.Abs(Quaternion.Angle(targetPoint.transform.rotation, transform.rotation));
            if(faceAngle != 0)
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
                npcState = NPCState.Walk;
            }
        }

        public void ReleaseToQueue()
        {
            numberOfNpcInQueue++;
            NextPoint();
            npcState = NPCState.Walk;
        }

        public void GetOrder()
        {
            if(npcState == NPCState.Order || npcState == NPCState.WaitOrder)
            {
                numberOfNpcInQueue--;
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
