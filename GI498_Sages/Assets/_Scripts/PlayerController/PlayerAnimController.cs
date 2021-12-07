using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public float speed = 0;

    private Animator animator;
    private string SPEED = "Speed";

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat(SPEED, speed);
    }


    public void SetTargetSpeed(Activity activity)
    {
        //use for change speed while not do anything
        switch (activity)
        {
            case Activity.Stand:
                speed = 0f;
                break;
            case Activity.Walk:
                speed = 0.5f;
                break;
            case Activity.Run:
                speed = 1f;
                break;
            // default:
            //     targetSpeed = 0f;
            //     break;
        }

        try
        {
            animator.SetFloat(SPEED, speed);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Data);
        }
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