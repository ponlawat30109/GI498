using UnityEngine;
using UnityEngine.UI;

public class MenuButtonClick : MonoBehaviour
{
    public AudioManager.Track sfxSound;

    void Start()
    {
        if(AudioManager.Instance != null) GetComponent<Button>().onClick.AddListener(()=>AudioManager.Instance.PlaySfx(sfxSound));
    }
}
