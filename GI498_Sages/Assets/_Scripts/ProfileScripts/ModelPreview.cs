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
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, defaulRotation, Time.deltaTime * rotationSpeed);
        }    
        
    }
}
