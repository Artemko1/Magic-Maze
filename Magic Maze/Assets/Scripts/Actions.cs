// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Actions.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Actions : IInputActionCollection
{
    private InputActionAsset asset;
    public Actions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""PlayerMap"",
            ""id"": ""6ac139bc-82ba-4e08-a28a-3a8ac69d61f2"",
            ""actions"": [
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""2b29749a-5660-4d4a-83db-2078c3baa540"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""8fe003fb-ade0-4189-a086-718f2a063463"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""385b247b-f403-41c8-9591-09a701a0981a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""9db79b54-1834-4dfd-bb5e-2eda5d087bf4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EndPlayerTurn"",
                    ""type"": ""Button"",
                    ""id"": ""b4c2d8b1-a7f3-4aef-88e1-cc56e26a9919"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""be14e5b1-3e27-42cb-85c9-4b232cf2b5eb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e895f68-f943-42c9-972a-aec70ff117ee"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""642497fa-d9f5-41fd-9709-4da744906b90"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a79d8a6-6a9e-41a4-8446-71f199e3395e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""994ca605-d156-42d8-a8ff-06e89e3f7d75"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab318fb1-a791-4f40-ab8f-fe5285fbc18f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d046ced-e848-4118-b30f-45998f16a0f4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""840b03c3-ac66-488f-a265-d511ed85434f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f0dcc38-7de1-4e9b-9e47-0b174bbe8161"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""EndPlayerTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ExcessTileMap"",
            ""id"": ""59713d72-8db0-43a4-b315-df93e5f521d1"",
            ""actions"": [
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""Button"",
                    ""id"": ""379558b0-ef42-49af-a3dd-e3662503d9b1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveBackward"",
                    ""type"": ""Button"",
                    ""id"": ""3a597cf2-ec38-4b02-b834-febcbf9ed3a3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateClockwise"",
                    ""type"": ""Button"",
                    ""id"": ""b3874625-0297-4900-b513-c066a2bda4ec"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCounterclockwise"",
                    ""type"": ""Button"",
                    ""id"": ""0cc639db-abce-4ae3-9216-989c39c058bf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveColumn"",
                    ""type"": ""Button"",
                    ""id"": ""f70c97f9-216c-4473-b3b2-f78101c459cd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b0c7c2d4-eaf1-445c-875f-08feff7d807e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c767d77e-7440-4bc8-a52c-17bfd1f359c1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveBackward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d3acfd1-3d6b-4262-8040-bd250e66e60e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""RotateClockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b172d339-3cc4-4071-a1a3-a54610b5b35d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""RotateCounterclockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afbad217-827f-47b3-8264-6a9ec51fba09"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveColumn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerMap
        m_PlayerMap = asset.GetActionMap("PlayerMap");
        m_PlayerMap_MoveUp = m_PlayerMap.GetAction("MoveUp");
        m_PlayerMap_MoveRight = m_PlayerMap.GetAction("MoveRight");
        m_PlayerMap_MoveDown = m_PlayerMap.GetAction("MoveDown");
        m_PlayerMap_MoveLeft = m_PlayerMap.GetAction("MoveLeft");
        m_PlayerMap_EndPlayerTurn = m_PlayerMap.GetAction("EndPlayerTurn");
        // ExcessTileMap
        m_ExcessTileMap = asset.GetActionMap("ExcessTileMap");
        m_ExcessTileMap_MoveForward = m_ExcessTileMap.GetAction("MoveForward");
        m_ExcessTileMap_MoveBackward = m_ExcessTileMap.GetAction("MoveBackward");
        m_ExcessTileMap_RotateClockwise = m_ExcessTileMap.GetAction("RotateClockwise");
        m_ExcessTileMap_RotateCounterclockwise = m_ExcessTileMap.GetAction("RotateCounterclockwise");
        m_ExcessTileMap_MoveColumn = m_ExcessTileMap.GetAction("MoveColumn");
    }

    ~Actions()
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

    // PlayerMap
    private readonly InputActionMap m_PlayerMap;
    private IPlayerMapActions m_PlayerMapActionsCallbackInterface;
    private readonly InputAction m_PlayerMap_MoveUp;
    private readonly InputAction m_PlayerMap_MoveRight;
    private readonly InputAction m_PlayerMap_MoveDown;
    private readonly InputAction m_PlayerMap_MoveLeft;
    private readonly InputAction m_PlayerMap_EndPlayerTurn;
    public struct PlayerMapActions
    {
        private Actions m_Wrapper;
        public PlayerMapActions(Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveUp => m_Wrapper.m_PlayerMap_MoveUp;
        public InputAction @MoveRight => m_Wrapper.m_PlayerMap_MoveRight;
        public InputAction @MoveDown => m_Wrapper.m_PlayerMap_MoveDown;
        public InputAction @MoveLeft => m_Wrapper.m_PlayerMap_MoveLeft;
        public InputAction @EndPlayerTurn => m_Wrapper.m_PlayerMap_EndPlayerTurn;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMapActions instance)
        {
            if (m_Wrapper.m_PlayerMapActionsCallbackInterface != null)
            {
                MoveUp.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveUp;
                MoveUp.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveUp;
                MoveUp.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveUp;
                MoveRight.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveRight;
                MoveRight.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveRight;
                MoveRight.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveRight;
                MoveDown.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveDown;
                MoveDown.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveDown;
                MoveDown.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveDown;
                MoveLeft.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveLeft;
                MoveLeft.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveLeft;
                MoveLeft.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveLeft;
                EndPlayerTurn.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnEndPlayerTurn;
                EndPlayerTurn.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnEndPlayerTurn;
                EndPlayerTurn.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnEndPlayerTurn;
            }
            m_Wrapper.m_PlayerMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                MoveUp.started += instance.OnMoveUp;
                MoveUp.performed += instance.OnMoveUp;
                MoveUp.canceled += instance.OnMoveUp;
                MoveRight.started += instance.OnMoveRight;
                MoveRight.performed += instance.OnMoveRight;
                MoveRight.canceled += instance.OnMoveRight;
                MoveDown.started += instance.OnMoveDown;
                MoveDown.performed += instance.OnMoveDown;
                MoveDown.canceled += instance.OnMoveDown;
                MoveLeft.started += instance.OnMoveLeft;
                MoveLeft.performed += instance.OnMoveLeft;
                MoveLeft.canceled += instance.OnMoveLeft;
                EndPlayerTurn.started += instance.OnEndPlayerTurn;
                EndPlayerTurn.performed += instance.OnEndPlayerTurn;
                EndPlayerTurn.canceled += instance.OnEndPlayerTurn;
            }
        }
    }
    public PlayerMapActions @PlayerMap => new PlayerMapActions(this);

    // ExcessTileMap
    private readonly InputActionMap m_ExcessTileMap;
    private IExcessTileMapActions m_ExcessTileMapActionsCallbackInterface;
    private readonly InputAction m_ExcessTileMap_MoveForward;
    private readonly InputAction m_ExcessTileMap_MoveBackward;
    private readonly InputAction m_ExcessTileMap_RotateClockwise;
    private readonly InputAction m_ExcessTileMap_RotateCounterclockwise;
    private readonly InputAction m_ExcessTileMap_MoveColumn;
    public struct ExcessTileMapActions
    {
        private Actions m_Wrapper;
        public ExcessTileMapActions(Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveForward => m_Wrapper.m_ExcessTileMap_MoveForward;
        public InputAction @MoveBackward => m_Wrapper.m_ExcessTileMap_MoveBackward;
        public InputAction @RotateClockwise => m_Wrapper.m_ExcessTileMap_RotateClockwise;
        public InputAction @RotateCounterclockwise => m_Wrapper.m_ExcessTileMap_RotateCounterclockwise;
        public InputAction @MoveColumn => m_Wrapper.m_ExcessTileMap_MoveColumn;
        public InputActionMap Get() { return m_Wrapper.m_ExcessTileMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ExcessTileMapActions set) { return set.Get(); }
        public void SetCallbacks(IExcessTileMapActions instance)
        {
            if (m_Wrapper.m_ExcessTileMapActionsCallbackInterface != null)
            {
                MoveForward.started -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnMoveForward;
                MoveForward.performed -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnMoveForward;
                MoveForward.canceled -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnMoveForward;
                MoveBackward.started -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnMoveBackward;
                MoveBackward.performed -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnMoveBackward;
                MoveBackward.canceled -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnMoveBackward;
                RotateClockwise.started -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnRotateClockwise;
                RotateClockwise.performed -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnRotateClockwise;
                RotateClockwise.canceled -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnRotateClockwise;
                RotateCounterclockwise.started -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnRotateCounterclockwise;
                RotateCounterclockwise.performed -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnRotateCounterclockwise;
                RotateCounterclockwise.canceled -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnRotateCounterclockwise;
                MoveColumn.started -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnMoveColumn;
                MoveColumn.performed -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnMoveColumn;
                MoveColumn.canceled -= m_Wrapper.m_ExcessTileMapActionsCallbackInterface.OnMoveColumn;
            }
            m_Wrapper.m_ExcessTileMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                MoveForward.started += instance.OnMoveForward;
                MoveForward.performed += instance.OnMoveForward;
                MoveForward.canceled += instance.OnMoveForward;
                MoveBackward.started += instance.OnMoveBackward;
                MoveBackward.performed += instance.OnMoveBackward;
                MoveBackward.canceled += instance.OnMoveBackward;
                RotateClockwise.started += instance.OnRotateClockwise;
                RotateClockwise.performed += instance.OnRotateClockwise;
                RotateClockwise.canceled += instance.OnRotateClockwise;
                RotateCounterclockwise.started += instance.OnRotateCounterclockwise;
                RotateCounterclockwise.performed += instance.OnRotateCounterclockwise;
                RotateCounterclockwise.canceled += instance.OnRotateCounterclockwise;
                MoveColumn.started += instance.OnMoveColumn;
                MoveColumn.performed += instance.OnMoveColumn;
                MoveColumn.canceled += instance.OnMoveColumn;
            }
        }
    }
    public ExcessTileMapActions @ExcessTileMap => new ExcessTileMapActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.GetControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.GetControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    public interface IPlayerMapActions
    {
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnEndPlayerTurn(InputAction.CallbackContext context);
    }
    public interface IExcessTileMapActions
    {
        void OnMoveForward(InputAction.CallbackContext context);
        void OnMoveBackward(InputAction.CallbackContext context);
        void OnRotateClockwise(InputAction.CallbackContext context);
        void OnRotateCounterclockwise(InputAction.CallbackContext context);
        void OnMoveColumn(InputAction.CallbackContext context);
    }
}
