using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
//using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
//public class PlayerController : MonoBehaviour, IPointerDownHandler
{
    public static PlayerController instance;

    // private NavMeshAgent _agent;
    [HideInInspector] public PlayerInput _playerInput;
    // Vector2 mousePosition;

    [SerializeField] CharacterController controller;
    private Vector3 playerVelocity;
    private bool ground;
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float gravity = -9.81f;
    // [SerializeField] private float jumpHeight = 0f;

    // [HideInInspector] public static bool kitchenOpenKey;
    // [HideInInspector] public static bool pickItemKey;
    // [HideInInspector] public static bool storageOpenKey;

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
        // _agent = GetComponent<NavMeshAgent>();

        // if(instance != null)
            instance = this;

        _playerInput = new PlayerInput();

        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // _playerInput.Mouse.MouseClick.performed += ctx => MouseAction();
    }

    void Update()
    {
        ground = controller.isGrounded;
        if (ground && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = _playerInput.Movement.OnScreenMove.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // // kitchenOpenKey = kitchenOpenKey? _playerInput.Movement.OpenKitchen.triggered : false;
        // if (_playerInput.Movement.OpenKitchen.triggered)
        // {
        //     kitchenOpenKey = true;
        // }

        // // pickItemKey = pickItemKey ? _playerInput.Movement.PickItem.triggered : false;
        // if (_playerInput.Movement.PickItem.triggered)
        // {
        //     pickItemKey = true;
        // }

        // // storageOpenKey = storageOpenKey? _playerInput.Movement.OpenStorage.triggered : false;
        // if (_playerInput.Movement.OpenStorage.triggered)
        // {
        //     storageOpenKey = true;
        // }

        // Debug.Log($"{kitchenOpenKey} {pickItemKey} {storageOpenKey}");

        // if (Input.GetButtonDown("Jump") && ground)
        // {
        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        // }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
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

    // void MouseAction()
    // {
    //     //mousePosition = _playerInput.Mouse.MousePosition.ReadValue<Vector2>();
    //     //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    //     mousePosition = Mouse.current.position.ReadValue();

    //     Ray _ray = Camera.main.ScreenPointToRay(mousePosition);
    //     RaycastHit _raycastHitInfo;

    //     if (Physics.Raycast(_ray, out _raycastHitInfo))
    //         if (_raycastHitInfo.transform.CompareTag("Floor"))
    //         {
    //             //Debug.Log(_raycastHitInfo.transform.tag);
    //             _agent.SetDestination(_raycastHitInfo.point);
    //         }
    // }
}
