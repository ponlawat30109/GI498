using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public float speed;
    public float targetSpeed;

    private Animator animator;
    private int speedHash;
    private bool onAccel = false;
    private float accel = 0.25f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
        speed = 0;
        targetSpeed = 0;
        animator.SetFloat(speedHash, speed);
    }

    private void Update()
    {
        //if (targetSpeed != speed)
        //{
        //    if (onAccel == true)
        //    {
        //        if (targetSpeed > speed)
        //        {
        //            targetSpeed = speed;
        //        }
        //        else
        //        {
        //            speed += accel;
        //        }
        //    }

        //    else //onAccel == false
        //    {
        //        if (targetSpeed < speed)
        //        {
        //            targetSpeed = speed;
        //        }
        //        else
        //        {
        //            speed -= accel;
        //        }
        //    }
        //    animator.SetFloat(speedHash, speed);
        //}
    }

    public void SetTargetSpeed(Activity activity)
    {
        //use for change speed while not do anything
        switch (activity)
        {
            case Activity.Stand:
                targetSpeed = 0f;
                break;
            case Activity.Walk:
                targetSpeed = 0.5f;
                break;
            case Activity.Run:
                targetSpeed = 1f;
                break;
            // default:
            //     targetSpeed = 0f;
            //     break;
        }
        onAccel = targetSpeed > speed;
        speed = targetSpeed;
        animator.SetFloat(speedHash, speed);
    }

    public void PickUp()
    {
        SetTargetSpeed(Activity.Stand);
        animator.SetTrigger("PickUp");
        animator.SetBool("CarryObj", true);
    }

    public void PickDown()
    {
        SetTargetSpeed(Activity.Stand);
        animator.SetTrigger("PickDown");
        animator.SetBool("CarryObj", false);
    }

    public void Move(Activity activity)
    {
        // Use for finish choping
        animator.SetTrigger("Move");
        SetTargetSpeed(activity);
    }

    public enum Activity
    {
        Stand,
        Walk,
        Run
    }
}