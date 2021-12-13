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

    [Header("Character Controller")]
    [SerializeField] private CharacterController controller;
    [HideInInspector] public Vector3 playerVelocity;
    private bool ground;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float gravity = -9.81f;
    private float currentPlayerspeed;
    [HideInInspector] public bool isRunning = false;
    // [SerializeField] private float jumpHeight = 0f;

    private Vector2 currentMovementInput;
    private Vector2 smoothInputVelocity;
    private float smoothInputSpeed = 0.2f;
     public bool isMoving = false;

    [Header("UI Handler")]
    public bool UIPanelActive = false;

    [Header("Player Anim")]
    [SerializeField] private PlayerAnimController animCtrl;

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
        _playerInput.Movement.Run.started += ctx =>
        {
            currentPlayerspeed = playerSpeed * 2;
            isRunning = true;
            animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Run);
        };
        _playerInput.Movement.Run.canceled += ctx =>
        {
            currentPlayerspeed = playerSpeed;
            isRunning = false;
            animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Walk);
        };
    }

    void Update()
    {
        // animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Stand);

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

        if (movementInput != Vector2.zero)
        {
            isMoving = true;
            animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Walk);
        }
        else
        {
            isMoving = false;
            animCtrl.SetTargetSpeed(PlayerAnimController.Activity.Stand);
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
