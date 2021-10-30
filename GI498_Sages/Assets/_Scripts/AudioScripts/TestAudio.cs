using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{

    private void OnGUI()
    {
        if(GUILayout.Button("Happy Music"))
        {
            AudioManager.Instance.PlayMusic(AudioManager.Track.BGMMenu01);
        }
        if (GUILayout.Button("Chocobo Music"))
        {
            AudioManager.Instance.PlayMusic(AudioManager.Track.BGMMenu02);
        }

    }
}
