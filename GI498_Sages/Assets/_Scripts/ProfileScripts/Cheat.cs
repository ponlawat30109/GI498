using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    [SerializeField] private InputField gainExp;
    [SerializeField] private Button savePlayerPrefButton;


    private const string GainExpKey = "GainExp";

    private void Start()
    {
        savePlayerPrefButton.onClick.AddListener(savePlayerPref);
    }

    private void savePlayerPref()
    {
        Debug.Log("savePlayerPref " + gainExp.text);
        PlayerPrefs.SetInt(GainExpKey, int.Parse(gainExp.text,System.Globalization.NumberStyles.AllowLeadingSign));
    }
}
