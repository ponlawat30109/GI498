using UnityEngine;
using UnityEngine.UI;

public class MenuButtonClick : MonoBehaviour
{
    public AudioManager.Track sfxSound;

    void Start()
    {
        if(AudioManager.Instance == null)
        {
            Debug.Log("AudioManager.Instance == null");
            return;
        }
        GetComponent<Button>().onClick.AddListener(()=>AudioManager.Instance.PlaySfx(sfxSound));
    }
}
