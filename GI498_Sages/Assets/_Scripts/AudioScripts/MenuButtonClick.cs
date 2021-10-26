using UnityEngine;
using UnityEngine.UI;

public class MenuButtonClick : MonoBehaviour
{
    public AudioManager.Track sfxSound;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=>AudioManager.Instance.PlaySfx(sfxSound));
    }
}
