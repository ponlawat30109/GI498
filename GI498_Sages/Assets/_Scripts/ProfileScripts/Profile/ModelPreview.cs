using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPreview : MonoBehaviour
{
    public bool onProfile;
    public bool onCustom;
    private Quaternion defaulRotation;
    private Quaternion lastRotation;
    public float rotationSpeed = 3f;

    private void Start()
    {
        onProfile = true;
        onCustom = false;
        defaulRotation = transform.rotation;
    }
    void Update()
    {
        if (onProfile)
        {
            onCustom = false;
            transform.Rotate(0, 0.1f, 0);
        }
        else if (!onCustom) //profile = false, custom = false
        {
            onCustom = true;
            lastRotation = transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, defaulRotation, Time.deltaTime * rotationSpeed);
        }
        else //profile = false, custom = true
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(30,new Vector3(0,1,0)), Time.deltaTime * rotationSpeed);
        }    
        
    }
}