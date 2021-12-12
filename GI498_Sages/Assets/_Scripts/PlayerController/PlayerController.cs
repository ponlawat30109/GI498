using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
//using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
//public class PlayerController : MonoBehaviour, IPointerDownHandler
{
    public static PlayerController instance;

    // private NavMeshAgent _agent;
    [SerializeField] public PlayerInput _playerInput;
    // Vector2 mousePosition;

    [SerializeField] private CharacterController controller;
    private Vector3 playerVelocity;
    private bool ground;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float gravity = -9.81f;
    float currentPlayerspeed;
    // [SerializeField] private float jumpHeight = 0f;

    private Vector2 currentMovementInput;
    private Vector2 smoothInputVelocity;
    private float smoothInputSpeed = 0.2f;

    // [HideInInspector] public static bool kitchenOpenKey;
    // [HideInInspector] public static bool pickItemKey;
    // [HideInInspector] public static bool storageOpenKey;

    public bool UIPanelActive = false;

    // private int idleTime = 5;
    // private float lastIdleTime;

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

        currentPlayerspeed = playerSpeed;
        // lastIdleTime = Time.time;
    }

    private void Start()
    {
        // _playerInput.Mouse.MouseClick.performed += ctx => MouseAction();
        _playerInput.Movement.Run.started += ctx => currentPlayerspeed = playerSpeed * 2;
        _playerInput.Movement.Run.canceled += ctx => currentPlayerspeed = playerSpeed;
    }

    void Update()
    {
        if (!UIPanelActive)
            Movement();

        if (_playerInput.Movement.Exit.triggered && UIPanelActive == false)
        {
            OnExitAction();
        }

        // if (Keyboard.current.anyKey.wasPressedThisFrame)
        // {
        //     lastIdleTime = Time.time;
        // }

        // if (IsIdle())
        // {
        //     Cursor.visible = false;
        // }
        // else
        // {
        //     Cursor.visible = true;
        // }
    }

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

    void Movement()
    {
        ground = controller.isGrounded;
        if (ground && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = _playerInput.Movement.OnScreenMove.ReadValue<Vector2>();
        currentMovementInput = Vector2.SmoothDamp(currentMovementInput, movementInput, ref smoothInputVelocity, smoothInputSpeed);
        Vector3 move = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);

        controller.Move(move * Time.deltaTime * currentPlayerspeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // if (Input.GetButtonDown("Jump") && ground)
        // {
        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        // }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void OnExitAction()
    {
        Debug.Log("Exit Kitchen");
        var loadScenes = new string[] { "scn_Profile" };
        var unloadScenes = new string[] { "KitchenAssembly" };
        // _Scripts.SceneAnimator.Instance.UnLoadScene();
        _Scripts.SceneAnimator.Instance.ChangeScene(unloadScenes, loadScenes);
    }

    // public bool IsIdle()
    // {
    //     return Time.time - lastIdleTime > idleTime;
    // }
}
