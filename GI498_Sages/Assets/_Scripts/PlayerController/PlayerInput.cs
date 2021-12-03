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
                    ""id"": ""9a9b9c9c-9569-45ae-abb4-ec0d9be22722"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0587fff-fcd4-4adf-9e00-d48eb0768c21"",
                    ""path"": ""<Mouse>/position"",
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
                    ""id"": ""3fdf19cf-f654-4014-bc09-f2074aeaf7d4"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenStorage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
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
    public struct MovementActions
    {
        private @PlayerInput m_Wrapper;
        public MovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @OnScreenMove => m_Wrapper.m_Movement_OnScreenMove;
        public InputAction @OpenKitchen => m_Wrapper.m_Movement_OpenKitchen;
        public InputAction @PickItem => m_Wrapper.m_Movement_PickItem;
        public InputAction @OpenStorage => m_Wrapper.m_Movement_OpenStorage;
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
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
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
    }
}
