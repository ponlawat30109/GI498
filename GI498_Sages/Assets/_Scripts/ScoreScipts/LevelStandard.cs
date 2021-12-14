using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Scoring Criteria/Level")]
public class LevelStandard : ScriptableObject
{
    public string sickness = "none";
    public string sex;
    public string age = "adult"; //default
    public float weight = 50; //default
    public LimiterSet limiterSet = new LimiterSet();
}