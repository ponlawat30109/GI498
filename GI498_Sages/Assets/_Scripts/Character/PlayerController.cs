
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
//using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
//public class PlayerController : MonoBehaviour, IPointerDownHandler
{
    private NavMeshAgent _agent;
    PlayerInput _playerInput;
    Vector2 mousePosition;

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        _playerInput.Mouse.MouseClick.performed += ctx => MouseAction();
    }

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         Ray _ray = Camera.main.ScreenPointToRay(mousePosition);
    //         RaycastHit _raycastHitInfo;

    //         if (Physics.Raycast(_ray, out _raycastHitInfo))
    //            _agent.SetDestination(_raycastHitInfo.point);
    //     }
    // }

    void MouseAction()
    {
        //mousePosition = _playerInput.Mouse.MousePosition.ReadValue<Vector2>();
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = Mouse.current.position.ReadValue();

        Ray _ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit _raycastHitInfo;

        if (Physics.Raycast(_ray, out _raycastHitInfo))
            if (_raycastHitInfo.transform.CompareTag("Floor"))
            {
                //Debug.Log(_raycastHitInfo.transform.tag);
                _agent.SetDestination(_raycastHitInfo.point);
            }
    }
}
