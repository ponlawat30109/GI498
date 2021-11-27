
using UnityEngine;
using Cinemachine;

public class ZoomModule : MonoBehaviour
{
    private CinemachineInputProvider _cInput;
    private CinemachineVirtualCamera _vcam;

    [SerializeField] private float zoomSpeed = 2;
    [SerializeField] private float minFOV = 50;
    [SerializeField] private float maxFOV = 70;

    void Awake()
    {
        _cInput = GetComponent<CinemachineInputProvider>();
        _vcam = GetComponent<CinemachineVirtualCamera>();
        // _vcam.m_Lens.FieldOfView = maxFOV;

        _vcam.m_Lens.FieldOfView = 60;
    }

    void Update()
    {
        float x = _cInput.GetAxisValue(0);
        float y = _cInput.GetAxisValue(1);
        float z = _cInput.GetAxisValue(2);

        if (z != 0)
        {
            ZoomScreen(z);
        }
    }

    void ZoomScreen(float increment)
    {
        float fov = _vcam.m_Lens.FieldOfView;
        float target = Mathf.Clamp(fov + increment, maxFOV, minFOV);

        _vcam.m_Lens.FieldOfView = Mathf.Lerp(fov, target, zoomSpeed * Time.deltaTime);
    }
}
