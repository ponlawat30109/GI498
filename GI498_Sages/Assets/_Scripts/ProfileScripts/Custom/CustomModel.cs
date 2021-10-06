using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomModel : MonoBehaviour
{
    public static CustomModel Instance;
    [SerializeField] private GameObject[] bodys;
    [SerializeField] private GameObject[] hairs;
    [SerializeField] private GameObject[] faces;
    [SerializeField] private GameObject[] outfits;
    [SerializeField] private GameObject[] hats;

    public void GetModel(out GameObject[] bodysArray, out GameObject[] hairsArray, out GameObject[] facesArray, out GameObject[] outfitsArray, out GameObject[] hatsArray)
    {
        bodysArray = bodys;
        hairsArray = hairs;
        facesArray = faces;
        outfitsArray = outfits;
        hatsArray = hats;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}