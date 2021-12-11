// GENERATED AUTOMATICALLY FROM 'Assets/_Scripts/PlayerController/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""7a6697cc-04a7-45ca-bac0-a6d413571c39"",
            ""actions"": [
                {
                    ""name"": ""Pan"",
                    ""type"": ""PassThrough"",
                    ""id"": ""aaee2b05-37e8-4cf0-9e46-10407dcd07b0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3643a3a5-ce49-4aba-b78a-23e19d60429b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""ea177c04-0363-4880-8493-bd8eca332ebe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""740b6a3e-aadc-42ca-b95c-1e89fde5a678"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e8cd3dde-7787-49c8-8c3d-145eed2509c4"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0587fff-fcd4-4adf-9e00-d48eb0768c21"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96b11256-8e38-4c01-aeb7-a200f00feefb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press,MultiTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbecc9f0-6972-4224-b2aa-8856f1fd0f5e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Movement"",
            ""id"": ""a478b009-6e8e-4b02-bf07-f1d879bf44e4"",
            ""actions"": [
                {
                    ""name"": ""OnScreenMove"",
                    ""type"": ""Value"",
                    ""id"": ""b13d6b0e-4ffb-429c-bfd1-82a73216247d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenKitchen"",
                    ""type"": ""Button"",
                    ""id"": ""787e6413-5001-4a8c-a5c3-710f48dfaeb5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PickItem"",
                    ""type"": ""Button"",
                    ""id"": ""ad8bc82c-024c-452c-9cb1-d2176d7a6447"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenStorage"",
                    ""type"": ""Button"",
                    ""id"": ""0a9fbd96-a0f1-4ba1-9905-3fb2db9b5be4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""09fa818d-2d2a-4e83-a70e-1a9d3f3eb544"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""fbd53450-fd3d-4205-81a1-181baa4da8ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f0cf3898-eb2c-4d91-8fb8-22ad87e3fcce"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnScreenMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""111331e3-605b-4a76-9a34-0fbc13dd8b67"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnScreenMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a5695e1b-ecd8-4c17-8cc2-e8cadf8af876"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnScreenMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""78be3b42-d496-4485-8931-e8fe0eeb87d7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnScreenMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b2f98920-1779-4fb6-8d59-72014fd2abb6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnScreenMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d61b337b-25e4-4a7b-b03d-c6651f5b58e1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnScreenMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b35d410e-3d26-4f52-84cf-d744e9996cd3"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenKitchen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32375824-b332-411b-aa70-fb099b061a70"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenKitchen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d55b0e1a-6857-4fce-933f-f37d3118b670"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb9141ca-7b1c-48ce-ab91-07fee4443d7e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fdf19cf-f654-4014-bc09-f2074aeaf7d4"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenStorage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8eecbc1c-56d4-43d2-b176-4e332fc58806"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenStorage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""260941b2-65c2-4063-9cca-e930e724a254"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8fb5f5b-4cb8-430b-bd04-ba86832f8829"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0ded1b5-26ab-4f56-a610-d3b698ff5950"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7807aafc-7cd5-40fd-bd25-de59e59aa869"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""c2dbd0ec-aa01-412a-a9ca-e761eeda52e5"",
            ""actions"": [
                {
                    ""name"": ""ClosePanel"",
                    ""type"": ""Button"",
                    ""id"": ""23e21cf9-bb6d-4267-9ad5-dd1ab6921090"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""45c70632-64c7-4547-9a7c-883c815b34eb"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClosePanel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca08edd0-5f81-4717-b482-56876eacfefe"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClosePanel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_Pan = m_Mouse.FindAction("Pan", throwIfNotFound: true);
        m_Mouse_Zoom = m_Mouse.FindAction("Zoom", throwIfNotFound: true);
        m_Mouse_MouseClick = m_Mouse.FindAction("MouseClick", throwIfNotFound: true);
        m_Mouse_MousePosition = m_Mouse.FindAction("MousePosition", throwIfNotFound: true);
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_OnScreenMove = m_Movement.FindAction("OnScreenMove", throwIfNotFound: true);
        m_Movement_OpenKitchen = m_Movement.FindAction("OpenKitchen", throwIfNotFound: true);
        m_Movement_PickItem = m_Movement.FindAction("PickItem", throwIfNotFound: true);
        m_Movement_OpenStorage = m_Movement.FindAction("OpenStorage", throwIfNotFound: true);
        m_Movement_Run = m_Movement.FindAction("Run", throwIfNotFound: true);
        m_Movement_Exit = m_Movement.FindAction("Exit", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_ClosePanel = m_UI.FindAction("ClosePanel", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_Pan;
    private readonly InputAction m_Mouse_Zoom;
    private readonly InputAction m_Mouse_MouseClick;
    private readonly InputAction m_Mouse_MousePosition;
    public struct MouseActions
    {
        private @PlayerInput m_Wrapper;
        public MouseActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pan => m_Wrapper.m_Mouse_Pan;
        public InputAction @Zoom => m_Wrapper.m_Mouse_Zoom;
        public InputAction @MouseClick => m_Wrapper.m_Mouse_MouseClick;
        public InputAction @MousePosition => m_Wrapper.m_Mouse_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @Pan.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnPan;
                @Pan.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnPan;
                @Pan.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnPan;
                @Zoom.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnZoom;
                @MouseClick.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMouseClick;
                @MousePosition.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pan.started += instance.OnPan;
                @Pan.performed += instance.OnPan;
                @Pan.canceled += instance.OnPan;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_OnScreenMove;
    private readonly InputAction m_Movement_OpenKitchen;
    private readonly InputAction m_Movement_PickItem;
    private readonly InputAction m_Movement_OpenStorage;
    private readonly InputAction m_Movement_Run;
    private readonly InputAction m_Movement_Exit;
    public struct MovementActions
    {
        private @PlayerInput m_Wrapper;
        public MovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @OnScreenMove => m_Wrapper.m_Movement_OnScreenMove;
        public InputAction @OpenKitchen => m_Wrapper.m_Movement_OpenKitchen;
        public InputAction @PickItem => m_Wrapper.m_Movement_PickItem;
        public InputAction @OpenStorage => m_Wrapper.m_Movement_OpenStorage;
        public InputAction @Run => m_Wrapper.m_Movement_Run;
        public InputAction @Exit => m_Wrapper.m_Movement_Exit;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @OnScreenMove.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnOnScreenMove;
                @OnScreenMove.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnOnScreenMove;
                @OnScreenMove.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnOnScreenMove;
                @OpenKitchen.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnOpenKitchen;
                @OpenKitchen.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnOpenKitchen;
                @OpenKitchen.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnOpenKitchen;
                @PickItem.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnPickItem;
                @PickItem.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnPickItem;
                @PickItem.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnPickItem;
                @OpenStorage.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnOpenStorage;
                @OpenStorage.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnOpenStorage;
                @OpenStorage.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnOpenStorage;
                @Run.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRun;
                @Exit.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnExit;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OnScreenMove.started += instance.OnOnScreenMove;
                @OnScreenMove.performed += instance.OnOnScreenMove;
                @OnScreenMove.canceled += instance.OnOnScreenMove;
                @OpenKitchen.started += instance.OnOpenKitchen;
                @OpenKitchen.performed += instance.OnOpenKitchen;
                @OpenKitchen.canceled += instance.OnOpenKitchen;
                @PickItem.started += instance.OnPickItem;
                @PickItem.performed += instance.OnPickItem;
                @PickItem.canceled += instance.OnPickItem;
                @OpenStorage.started += instance.OnOpenStorage;
                @OpenStorage.performed += instance.OnOpenStorage;
                @OpenStorage.canceled += instance.OnOpenStorage;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_ClosePanel;
    public struct UIActions
    {
        private @PlayerInput m_Wrapper;
        public UIActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ClosePanel => m_Wrapper.m_UI_ClosePanel;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @ClosePanel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClosePanel;
                @ClosePanel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClosePanel;
                @ClosePanel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClosePanel;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ClosePanel.started += instance.OnClosePanel;
                @ClosePanel.performed += instance.OnClosePanel;
                @ClosePanel.canceled += instance.OnClosePanel;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IMouseActions
    {
        void OnPan(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
    public interface IMovementActions
    {
        void OnOnScreenMove(InputAction.CallbackContext context);
        void OnOpenKitchen(InputAction.CallbackContext context);
        void OnPickItem(InputAction.CallbackContext context);
        void OnOpenStorage(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnClosePanel(InputAction.CallbackContext context);
    }
}
