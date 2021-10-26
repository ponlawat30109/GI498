using UnityEngine;
using UnityEngine.UI;

public class SfxSlider : MonoBehaviour
{
    public MenuController menuCtrl;
    public float delayTime = 0.5f;
    public float time;
    public bool onChanged;
    public bool firstCheck;

    void Start()
    {
        time = delayTime;
        onChanged = false;
        firstCheck = false;
        GetComponent<Slider>().onValueChanged.AddListener((volumn) => SliderValueChange(volumn));
    }

    void Update()
    {
        if (onChanged == true)
        {
            time += Time.deltaTime;
            if (time >= delayTime)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Track.ClickButton01);
                onChanged = false;
                time = 0;
            }
        }
    }

    void SliderValueChange(float volumn)
    {
        menuCtrl.SetOption("sound", volumn);
        if (firstCheck == true)
            onChanged = true;
        else
        {
            onChanged = false;
            firstCheck = true;
        }
    }
}
