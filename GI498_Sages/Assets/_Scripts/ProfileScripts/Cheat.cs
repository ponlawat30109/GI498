using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    [SerializeField] private PlayerProfile playerProfile;
    private const string GainExpKey = "GainExp";
    private string gainXp;

    private void OnGUI()
    {
        gainXp = GUILayout.TextField(gainXp);
        if (GUILayout.Button("Save gainXP"))
        {
            Debug.Log("savePlayerPref " + gainXp);
            PlayerPrefs.SetInt(GainExpKey, int.Parse(gainXp, System.Globalization.NumberStyles.AllowLeadingSign));
        }

        if (GUILayout.Button("ResetProfile"))
            playerProfile.ResetProfile();
    }

}
