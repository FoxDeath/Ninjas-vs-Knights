// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/PlayerInput.inputactions'

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
            ""name"": ""Ninja"",
            ""id"": ""3ca94b9f-d444-4775-ba40-48dfbfe5135c"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""3d8e595e-6322-46c8-aebf-d75597715fe5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""162691f4-2fe3-44a8-9b80-6b661fed831f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""19193a2f-8a8d-4354-9b91-d472e3cc1484"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""0f35ab6f-75df-461c-9f0e-051d7a62d3a9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""174db001-2c17-4ee9-b135-01e3d5573dad"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scope"",
                    ""type"": ""Button"",
                    ""id"": ""3558cc0b-596e-468e-b70c-d418962defaf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""2458a1fb-d161-492e-9046-eaec27acac96"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grapling Hook"",
                    ""type"": ""Button"",
                    ""id"": ""4c0a85ae-4079-45df-bd15-ebea32cffde3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""8f4b225b-234d-4413-b343-145b6a96b3c3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fd911d69-d345-4717-a435-f39617209e95"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""8796cef5-c631-4910-af80-ac53573bde4b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9a01ef79-628d-41d4-a334-b486016fad31"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4cf5353d-fe72-4883-9ab8-2d70f4afc3ac"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f1cd8abd-f175-4e06-9479-5083ad00e0f6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""48d98b1b-288a-4c51-b6a0-88b44172b72f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""96e76c42-87f1-48ef-b01d-3cbe3668f47f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4df8866-84ea-4aae-b303-983e451ea9c1"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbec5257-5027-498f-a9e2-702e97f2f45e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d185bede-461d-4e83-832f-e5ec3b55d642"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scope"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b40185fc-6e07-4f54-b962-f299bb545fc8"",
                    ""path"": ""<Keyboard>/#(R)"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36796939-65e8-4c7c-94c2-e9452eb1a729"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapling Hook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2f33c4b-b591-4aef-9388-d37f9086e52d"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Knight"",
            ""id"": ""75620fe3-3537-4cbf-beb0-34235a9b816d"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""f3881dd5-c9bc-42b4-9b39-b25ffa9635cb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""13e8d25b-0bfe-41a5-88e0-0cac72d74e1e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""4c9b6429-bd9c-465c-9e28-b4549d5f0997"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jetpack Dash"",
                    ""type"": ""Button"",
                    ""id"": ""3dd231e9-759b-4ebc-90db-879e87f7c9d9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8f80fc03-3940-4607-8564-c3746f1daabe"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jetpack Jump"",
                    ""type"": ""Button"",
                    ""id"": ""0de5b342-1f49-4aef-8977-d036a7d5e24b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""f55b3568-3a37-4acc-8116-feb32b73bab9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""999c697c-fa11-4df5-8f21-296afb5d0fe9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ac083a0-9a04-4ec2-9919-cd3caa11dc8b"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""cee7bd15-1089-4907-8244-80004dceb87f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4a899fcd-3cbb-4a71-a4bf-4d4c4ca8919d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""efb79e6a-f230-428e-811c-baee3d81813b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1098bb7a-40c1-4972-aeed-8855ff53c7c0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9b82cc51-bd14-45b5-82d2-3304e22062fc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""12564c05-902f-44dc-a813-84b69ad9a1a2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ed5a03e-caef-41b5-bb92-dc53e9570112"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7489b9e-8963-45d0-b4e2-93112496242e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jetpack Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8020a29f-758d-4ed9-af8c-710d9b195ee0"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""MultiTap(tapDelay=0.2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jetpack Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
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
        }
    ]
}");
        // Ninja
        m_Ninja = asset.FindActionMap("Ninja", throwIfNotFound: true);
        m_Ninja_Look = m_Ninja.FindAction("Look", throwIfNotFound: true);
        m_Ninja_Movement = m_Ninja.FindAction("Movement", throwIfNotFound: true);
        m_Ninja_Jump = m_Ninja.FindAction("Jump", throwIfNotFound: true);
        m_Ninja_Sprint = m_Ninja.FindAction("Sprint", throwIfNotFound: true);
        m_Ninja_Fire = m_Ninja.FindAction("Fire", throwIfNotFound: true);
        m_Ninja_Scope = m_Ninja.FindAction("Scope", throwIfNotFound: true);
        m_Ninja_Reload = m_Ninja.FindAction("Reload", throwIfNotFound: true);
        m_Ninja_GraplingHook = m_Ninja.FindAction("Grapling Hook", throwIfNotFound: true);
        m_Ninja_Crouch = m_Ninja.FindAction("Crouch", throwIfNotFound: true);
        // Knight
        m_Knight = asset.FindActionMap("Knight", throwIfNotFound: true);
        m_Knight_Look = m_Knight.FindAction("Look", throwIfNotFound: true);
        m_Knight_Movement = m_Knight.FindAction("Movement", throwIfNotFound: true);
        m_Knight_Sprint = m_Knight.FindAction("Sprint", throwIfNotFound: true);
        m_Knight_JetpackDash = m_Knight.FindAction("Jetpack Dash", throwIfNotFound: true);
        m_Knight_Jump = m_Knight.FindAction("Jump", throwIfNotFound: true);
        m_Knight_JetpackJump = m_Knight.FindAction("Jetpack Jump", throwIfNotFound: true);
        m_Knight_Shoot = m_Knight.FindAction("Shoot", throwIfNotFound: true);
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

    // Ninja
    private readonly InputActionMap m_Ninja;
    private INinjaActions m_NinjaActionsCallbackInterface;
    private readonly InputAction m_Ninja_Look;
    private readonly InputAction m_Ninja_Movement;
    private readonly InputAction m_Ninja_Jump;
    private readonly InputAction m_Ninja_Sprint;
    private readonly InputAction m_Ninja_Fire;
    private readonly InputAction m_Ninja_Scope;
    private readonly InputAction m_Ninja_Reload;
    private readonly InputAction m_Ninja_GraplingHook;
    private readonly InputAction m_Ninja_Crouch;
    public struct NinjaActions
    {
        private @PlayerInput m_Wrapper;
        public NinjaActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Ninja_Look;
        public InputAction @Movement => m_Wrapper.m_Ninja_Movement;
        public InputAction @Jump => m_Wrapper.m_Ninja_Jump;
        public InputAction @Sprint => m_Wrapper.m_Ninja_Sprint;
        public InputAction @Fire => m_Wrapper.m_Ninja_Fire;
        public InputAction @Scope => m_Wrapper.m_Ninja_Scope;
        public InputAction @Reload => m_Wrapper.m_Ninja_Reload;
        public InputAction @GraplingHook => m_Wrapper.m_Ninja_GraplingHook;
        public InputAction @Crouch => m_Wrapper.m_Ninja_Crouch;
        public InputActionMap Get() { return m_Wrapper.m_Ninja; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NinjaActions set) { return set.Get(); }
        public void SetCallbacks(INinjaActions instance)
        {
            if (m_Wrapper.m_NinjaActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnLook;
                @Movement.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnSprint;
                @Fire.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnFire;
                @Scope.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnScope;
                @Scope.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnScope;
                @Scope.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnScope;
                @Reload.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnReload;
                @GraplingHook.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnGraplingHook;
                @GraplingHook.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnGraplingHook;
                @GraplingHook.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnGraplingHook;
                @Crouch.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnCrouch;
            }
            m_Wrapper.m_NinjaActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Scope.started += instance.OnScope;
                @Scope.performed += instance.OnScope;
                @Scope.canceled += instance.OnScope;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @GraplingHook.started += instance.OnGraplingHook;
                @GraplingHook.performed += instance.OnGraplingHook;
                @GraplingHook.canceled += instance.OnGraplingHook;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
            }
        }
    }
    public NinjaActions @Ninja => new NinjaActions(this);

    // Knight
    private readonly InputActionMap m_Knight;
    private IKnightActions m_KnightActionsCallbackInterface;
    private readonly InputAction m_Knight_Look;
    private readonly InputAction m_Knight_Movement;
    private readonly InputAction m_Knight_Sprint;
    private readonly InputAction m_Knight_JetpackDash;
    private readonly InputAction m_Knight_Jump;
    private readonly InputAction m_Knight_JetpackJump;
    private readonly InputAction m_Knight_Shoot;
    public struct KnightActions
    {
        private @PlayerInput m_Wrapper;
        public KnightActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Knight_Look;
        public InputAction @Movement => m_Wrapper.m_Knight_Movement;
        public InputAction @Sprint => m_Wrapper.m_Knight_Sprint;
        public InputAction @JetpackDash => m_Wrapper.m_Knight_JetpackDash;
        public InputAction @Jump => m_Wrapper.m_Knight_Jump;
        public InputAction @JetpackJump => m_Wrapper.m_Knight_JetpackJump;
        public InputAction @Shoot => m_Wrapper.m_Knight_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Knight; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KnightActions set) { return set.Get(); }
        public void SetCallbacks(IKnightActions instance)
        {
            if (m_Wrapper.m_KnightActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnLook;
                @Movement.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnMovement;
                @Sprint.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnSprint;
                @JetpackDash.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnJetpackDash;
                @JetpackDash.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnJetpackDash;
                @JetpackDash.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnJetpackDash;
                @Jump.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnJump;
                @JetpackJump.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnJetpackJump;
                @JetpackJump.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnJetpackJump;
                @JetpackJump.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnJetpackJump;
                @Shoot.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_KnightActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @JetpackDash.started += instance.OnJetpackDash;
                @JetpackDash.performed += instance.OnJetpackDash;
                @JetpackDash.canceled += instance.OnJetpackDash;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @JetpackJump.started += instance.OnJetpackJump;
                @JetpackJump.performed += instance.OnJetpackJump;
                @JetpackJump.canceled += instance.OnJetpackJump;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public KnightActions @Knight => new KnightActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface INinjaActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnScope(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnGraplingHook(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
    }
    public interface IKnightActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJetpackDash(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnJetpackJump(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}
