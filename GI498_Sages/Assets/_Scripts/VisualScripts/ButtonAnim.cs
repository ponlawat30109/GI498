using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[AddComponentMenu("_VisualMod/ButtonAnim")]
public class ButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Transform tf;
    private Vector3 defaultScale;
    public AudioManager.Track sfxSound;
    public float zoomButtonScale = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        defaultScale = tf.localScale;
        //if (AudioManager.Instance != null) GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlaySfx(sfxSound));
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        tf.localScale = defaultScale;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        tf.localScale = defaultScale * zoomButtonScale;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        tf.localScale = defaultScale;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        tf.localScale = defaultScale * zoomButtonScale;
        AudioManager.Instance.PlaySfx(sfxSound);
    }

}
