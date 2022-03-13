//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/MyAssets/InputSystem/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace URPMk2
{
    public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Controllable"",
            ""id"": ""9c605f07-25ba-4c78-b59d-e42bbdd95e6c"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""fbc27592-1700-448b-8037-f147bc7d0214"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""623d70ba-f635-4372-8258-fbae3a7e5929"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""207da09e-bb0e-4bb4-ba98-fe5cf5e8decc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""810eb233-b1d6-49e2-bdf6-feec6e5219e5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b288906d-2cff-41e2-9763-70d72bef5b4e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a15c0794-6f52-42fa-8168-98676fe64e27"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WSAD"",
                    ""id"": ""8fbca4b4-3645-49da-91c7-29b4217e4af1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""eff0a16e-a30e-4962-83db-e868ad5c5def"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""87b39951-afe4-4461-b3fc-9465a98001b3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""47cb8564-7f25-463e-be47-287865b11cef"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0cbbe058-ba5f-45fd-9ea9-4106b8799ecb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8f0dbdcc-375d-4f26-80f6-9cc92449d110"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38c4a958-fdbb-4944-90d1-6158f481c0ed"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02f7e0dd-afb8-47de-a5be-ab0d3c4669c8"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Humanoid"",
            ""id"": ""5608d37f-2245-4511-8c93-89daf35c07e2"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""1d131a1a-488a-4151-8110-84b9a28751db"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RunStart"",
                    ""type"": ""Button"",
                    ""id"": ""dc2cdf78-502f-46b3-975c-355cb0664763"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RunFinish"",
                    ""type"": ""Button"",
                    ""id"": ""a7630a66-b323-4c1d-8fcd-5862135f25d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7e64ed83-52ef-4d0d-b87e-c7c4af527883"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f6a54e77-6abb-436b-9f3f-435ccc87f048"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""861deee6-e9fe-4edc-9353-52c9e62510e9"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WSAD"",
                    ""id"": ""e43f5b59-88b0-4d6b-ab0d-c50f09ab5c5f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""85681ee5-de16-474e-8f77-e4a9db240cc5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""da63512f-6ff4-4ef5-8a04-e77fba0a316c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ed30353b-ce44-4aca-810b-d4ebacd1c17b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b4b90aa9-64ac-417d-b6da-9a0bf07b6a82"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""48c18255-8ac4-42b1-a38d-8c7378d827fd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0c4493dc-e33e-46ed-8252-056512667bbd"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a996dd39-c49c-4162-bf11-e0f442619f27"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2c7ff52f-c2e5-4ca0-b8dc-5d1a62dc769f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9d47fc54-c1de-4a7b-855e-39d60e3ef0b7"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""182980dc-caba-4ccb-b3d5-7ff1e90f525a"",
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
                    ""id"": ""eb877596-1249-437a-8c9d-440368dbf204"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RunStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29aec921-6ac5-403a-91aa-eb004f0f727a"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a011140a-46b9-4040-a9c1-e2551f0f224b"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c1cc09c-9b33-4f91-9beb-5ff85e882853"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RunFinish"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Controllable
            m_Controllable = asset.FindActionMap("Controllable", throwIfNotFound: true);
            m_Controllable_Jump = m_Controllable.FindAction("Jump", throwIfNotFound: true);
            m_Controllable_Run = m_Controllable.FindAction("Run", throwIfNotFound: true);
            m_Controllable_Move = m_Controllable.FindAction("Move", throwIfNotFound: true);
            m_Controllable_MouseX = m_Controllable.FindAction("MouseX", throwIfNotFound: true);
            m_Controllable_MouseY = m_Controllable.FindAction("MouseY", throwIfNotFound: true);
            // Humanoid
            m_Humanoid = asset.FindActionMap("Humanoid", throwIfNotFound: true);
            m_Humanoid_Move = m_Humanoid.FindAction("Move", throwIfNotFound: true);
            m_Humanoid_RunStart = m_Humanoid.FindAction("RunStart", throwIfNotFound: true);
            m_Humanoid_RunFinish = m_Humanoid.FindAction("RunFinish", throwIfNotFound: true);
            m_Humanoid_Jump = m_Humanoid.FindAction("Jump", throwIfNotFound: true);
            m_Humanoid_MouseX = m_Humanoid.FindAction("MouseX", throwIfNotFound: true);
            m_Humanoid_MouseY = m_Humanoid.FindAction("MouseY", throwIfNotFound: true);
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
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Controllable
        private readonly InputActionMap m_Controllable;
        private IControllableActions m_ControllableActionsCallbackInterface;
        private readonly InputAction m_Controllable_Jump;
        private readonly InputAction m_Controllable_Run;
        private readonly InputAction m_Controllable_Move;
        private readonly InputAction m_Controllable_MouseX;
        private readonly InputAction m_Controllable_MouseY;
        public struct ControllableActions
        {
            private @PlayerInputActions m_Wrapper;
            public ControllableActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Jump => m_Wrapper.m_Controllable_Jump;
            public InputAction @Run => m_Wrapper.m_Controllable_Run;
            public InputAction @Move => m_Wrapper.m_Controllable_Move;
            public InputAction @MouseX => m_Wrapper.m_Controllable_MouseX;
            public InputAction @MouseY => m_Wrapper.m_Controllable_MouseY;
            public InputActionMap Get() { return m_Wrapper.m_Controllable; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ControllableActions set) { return set.Get(); }
            public void SetCallbacks(IControllableActions instance)
            {
                if (m_Wrapper.m_ControllableActionsCallbackInterface != null)
                {
                    @Jump.started -= m_Wrapper.m_ControllableActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_ControllableActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_ControllableActionsCallbackInterface.OnJump;
                    @Run.started -= m_Wrapper.m_ControllableActionsCallbackInterface.OnRun;
                    @Run.performed -= m_Wrapper.m_ControllableActionsCallbackInterface.OnRun;
                    @Run.canceled -= m_Wrapper.m_ControllableActionsCallbackInterface.OnRun;
                    @Move.started -= m_Wrapper.m_ControllableActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_ControllableActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_ControllableActionsCallbackInterface.OnMove;
                    @MouseX.started -= m_Wrapper.m_ControllableActionsCallbackInterface.OnMouseX;
                    @MouseX.performed -= m_Wrapper.m_ControllableActionsCallbackInterface.OnMouseX;
                    @MouseX.canceled -= m_Wrapper.m_ControllableActionsCallbackInterface.OnMouseX;
                    @MouseY.started -= m_Wrapper.m_ControllableActionsCallbackInterface.OnMouseY;
                    @MouseY.performed -= m_Wrapper.m_ControllableActionsCallbackInterface.OnMouseY;
                    @MouseY.canceled -= m_Wrapper.m_ControllableActionsCallbackInterface.OnMouseY;
                }
                m_Wrapper.m_ControllableActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Run.started += instance.OnRun;
                    @Run.performed += instance.OnRun;
                    @Run.canceled += instance.OnRun;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @MouseX.started += instance.OnMouseX;
                    @MouseX.performed += instance.OnMouseX;
                    @MouseX.canceled += instance.OnMouseX;
                    @MouseY.started += instance.OnMouseY;
                    @MouseY.performed += instance.OnMouseY;
                    @MouseY.canceled += instance.OnMouseY;
                }
            }
        }
        public ControllableActions @Controllable => new ControllableActions(this);

        // Humanoid
        private readonly InputActionMap m_Humanoid;
        private IHumanoidActions m_HumanoidActionsCallbackInterface;
        private readonly InputAction m_Humanoid_Move;
        private readonly InputAction m_Humanoid_RunStart;
        private readonly InputAction m_Humanoid_RunFinish;
        private readonly InputAction m_Humanoid_Jump;
        private readonly InputAction m_Humanoid_MouseX;
        private readonly InputAction m_Humanoid_MouseY;
        public struct HumanoidActions
        {
            private @PlayerInputActions m_Wrapper;
            public HumanoidActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Humanoid_Move;
            public InputAction @RunStart => m_Wrapper.m_Humanoid_RunStart;
            public InputAction @RunFinish => m_Wrapper.m_Humanoid_RunFinish;
            public InputAction @Jump => m_Wrapper.m_Humanoid_Jump;
            public InputAction @MouseX => m_Wrapper.m_Humanoid_MouseX;
            public InputAction @MouseY => m_Wrapper.m_Humanoid_MouseY;
            public InputActionMap Get() { return m_Wrapper.m_Humanoid; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(HumanoidActions set) { return set.Get(); }
            public void SetCallbacks(IHumanoidActions instance)
            {
                if (m_Wrapper.m_HumanoidActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnMove;
                    @RunStart.started -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnRunStart;
                    @RunStart.performed -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnRunStart;
                    @RunStart.canceled -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnRunStart;
                    @RunFinish.started -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnRunFinish;
                    @RunFinish.performed -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnRunFinish;
                    @RunFinish.canceled -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnRunFinish;
                    @Jump.started -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnJump;
                    @MouseX.started -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnMouseX;
                    @MouseX.performed -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnMouseX;
                    @MouseX.canceled -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnMouseX;
                    @MouseY.started -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnMouseY;
                    @MouseY.performed -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnMouseY;
                    @MouseY.canceled -= m_Wrapper.m_HumanoidActionsCallbackInterface.OnMouseY;
                }
                m_Wrapper.m_HumanoidActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @RunStart.started += instance.OnRunStart;
                    @RunStart.performed += instance.OnRunStart;
                    @RunStart.canceled += instance.OnRunStart;
                    @RunFinish.started += instance.OnRunFinish;
                    @RunFinish.performed += instance.OnRunFinish;
                    @RunFinish.canceled += instance.OnRunFinish;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @MouseX.started += instance.OnMouseX;
                    @MouseX.performed += instance.OnMouseX;
                    @MouseX.canceled += instance.OnMouseX;
                    @MouseY.started += instance.OnMouseY;
                    @MouseY.performed += instance.OnMouseY;
                    @MouseY.canceled += instance.OnMouseY;
                }
            }
        }
        public HumanoidActions @Humanoid => new HumanoidActions(this);
        public interface IControllableActions
        {
            void OnJump(InputAction.CallbackContext context);
            void OnRun(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
            void OnMouseX(InputAction.CallbackContext context);
            void OnMouseY(InputAction.CallbackContext context);
        }
        public interface IHumanoidActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnRunStart(InputAction.CallbackContext context);
            void OnRunFinish(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnMouseX(InputAction.CallbackContext context);
            void OnMouseY(InputAction.CallbackContext context);
        }
    }
}
