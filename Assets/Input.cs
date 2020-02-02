// GENERATED AUTOMATICALLY FROM 'Assets/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""InputPad"",
            ""id"": ""9808ad0b-bd04-4f9f-b09b-9eb92c64c4c9"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""36c5b04b-d0e8-4e50-b318-3771e31221ac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""3a989384-a630-4276-9c5b-be360a34cfe8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JetPack"",
                    ""type"": ""Button"",
                    ""id"": ""5e1a005f-757f-413e-82a0-3d54affb9ea6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""410a7e9f-0644-4639-8346-3d3bb37cb43d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9eb20b12-969d-441f-8068-c90d289a930e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b8c3a9c-c1f8-4cc8-b2d0-4773bc509496"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JetPack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""id"": ""9abe50eb-b678-4bb1-8c08-7e8728fcccc8"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4dc8adef-4d26-4eca-b6ac-7da17e8ed199"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JetPack"",
                    ""type"": ""Button"",
                    ""id"": ""8f964392-aae3-44ee-939e-04718f66b550"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RunLeft"",
                    ""type"": ""Button"",
                    ""id"": ""657c927f-cb12-455e-82f8-6f9f95eb7d2c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RunRight"",
                    ""type"": ""Button"",
                    ""id"": ""ad8cde03-5706-4d03-a3b3-26e6033327ad"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""867759e7-6d24-46d4-ac46-0042aad807e3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""079d4e51-aef9-4aff-bdbc-beb08be220d7"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JetPack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f59bed40-1b79-4fff-b981-63914c00ddbf"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RunLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65bdd51e-0fb7-4f2d-b8db-da28f1d83ab5"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RunRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InputPad
        m_InputPad = asset.FindActionMap("InputPad", throwIfNotFound: true);
        m_InputPad_Jump = m_InputPad.FindAction("Jump", throwIfNotFound: true);
        m_InputPad_Run = m_InputPad.FindAction("Run", throwIfNotFound: true);
        m_InputPad_JetPack = m_InputPad.FindAction("JetPack", throwIfNotFound: true);
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Jump = m_Keyboard.FindAction("Jump", throwIfNotFound: true);
        m_Keyboard_JetPack = m_Keyboard.FindAction("JetPack", throwIfNotFound: true);
        m_Keyboard_RunLeft = m_Keyboard.FindAction("RunLeft", throwIfNotFound: true);
        m_Keyboard_RunRight = m_Keyboard.FindAction("RunRight", throwIfNotFound: true);
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

    // InputPad
    private readonly InputActionMap m_InputPad;
    private IInputPadActions m_InputPadActionsCallbackInterface;
    private readonly InputAction m_InputPad_Jump;
    private readonly InputAction m_InputPad_Run;
    private readonly InputAction m_InputPad_JetPack;
    public struct InputPadActions
    {
        private @Input m_Wrapper;
        public InputPadActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_InputPad_Jump;
        public InputAction @Run => m_Wrapper.m_InputPad_Run;
        public InputAction @JetPack => m_Wrapper.m_InputPad_JetPack;
        public InputActionMap Get() { return m_Wrapper.m_InputPad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputPadActions set) { return set.Get(); }
        public void SetCallbacks(IInputPadActions instance)
        {
            if (m_Wrapper.m_InputPadActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_InputPadActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_InputPadActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_InputPadActionsCallbackInterface.OnJump;
                @Run.started -= m_Wrapper.m_InputPadActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_InputPadActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_InputPadActionsCallbackInterface.OnRun;
                @JetPack.started -= m_Wrapper.m_InputPadActionsCallbackInterface.OnJetPack;
                @JetPack.performed -= m_Wrapper.m_InputPadActionsCallbackInterface.OnJetPack;
                @JetPack.canceled -= m_Wrapper.m_InputPadActionsCallbackInterface.OnJetPack;
            }
            m_Wrapper.m_InputPadActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @JetPack.started += instance.OnJetPack;
                @JetPack.performed += instance.OnJetPack;
                @JetPack.canceled += instance.OnJetPack;
            }
        }
    }
    public InputPadActions @InputPad => new InputPadActions(this);

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Jump;
    private readonly InputAction m_Keyboard_JetPack;
    private readonly InputAction m_Keyboard_RunLeft;
    private readonly InputAction m_Keyboard_RunRight;
    public struct KeyboardActions
    {
        private @Input m_Wrapper;
        public KeyboardActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Keyboard_Jump;
        public InputAction @JetPack => m_Wrapper.m_Keyboard_JetPack;
        public InputAction @RunLeft => m_Wrapper.m_Keyboard_RunLeft;
        public InputAction @RunRight => m_Wrapper.m_Keyboard_RunRight;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJump;
                @JetPack.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJetPack;
                @JetPack.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJetPack;
                @JetPack.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJetPack;
                @RunLeft.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRunLeft;
                @RunLeft.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRunLeft;
                @RunLeft.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRunLeft;
                @RunRight.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRunRight;
                @RunRight.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRunRight;
                @RunRight.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRunRight;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @JetPack.started += instance.OnJetPack;
                @JetPack.performed += instance.OnJetPack;
                @JetPack.canceled += instance.OnJetPack;
                @RunLeft.started += instance.OnRunLeft;
                @RunLeft.performed += instance.OnRunLeft;
                @RunLeft.canceled += instance.OnRunLeft;
                @RunRight.started += instance.OnRunRight;
                @RunRight.performed += instance.OnRunRight;
                @RunRight.canceled += instance.OnRunRight;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IInputPadActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJetPack(InputAction.CallbackContext context);
    }
    public interface IKeyboardActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnJetPack(InputAction.CallbackContext context);
        void OnRunLeft(InputAction.CallbackContext context);
        void OnRunRight(InputAction.CallbackContext context);
    }
}
