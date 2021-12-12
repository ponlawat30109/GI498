using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerActive : MonoBehaviour
{
    public Outline outlineScripts;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            outlineScripts.GetComponent<Outline>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            outlineScripts.GetComponent<Outline>().enabled = false;
        }
    }
}
