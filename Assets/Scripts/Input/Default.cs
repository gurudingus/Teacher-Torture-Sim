//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/Default.inputactions
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

public partial class @Default: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Default()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Default"",
    ""maps"": [
        {
            ""name"": ""Interactions"",
            ""id"": ""ca65c367-ad1f-41a7-b250-da03a059cae5"",
            ""actions"": [
                {
                    ""name"": ""HandLeft"",
                    ""type"": ""Value"",
                    ""id"": ""54d16d58-8ca3-4e7a-bb38-64c878af91a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""HandRight"",
                    ""type"": ""Value"",
                    ""id"": ""e41b0e71-5f9c-4639-bfa6-8434febf144c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""53e41e15-d950-4fd8-99b5-ed1db44594a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73ab66e6-39d3-4f90-bf75-2bf2df75dd84"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7f35f93-f965-4b35-88bc-554efc7c0d0a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HandRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c83b54e9-6442-49a6-ab6f-ba8eb8f453f4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HandLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Other"",
            ""id"": ""0251f851-be4d-459b-b82e-878bc0c6531b"",
            ""actions"": [
                {
                    ""name"": ""SkipCutscene"",
                    ""type"": ""Value"",
                    ""id"": ""fb27561f-ebce-4842-9920-c9a6964a1624"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""a6d94bd4-2888-4730-963c-afd040338348"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5f8dce14-59d1-4739-bbfc-9190c92af213"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipCutscene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0716890-ed65-4f76-a889-4a1e9c88055f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Interactions
        m_Interactions = asset.FindActionMap("Interactions", throwIfNotFound: true);
        m_Interactions_HandLeft = m_Interactions.FindAction("HandLeft", throwIfNotFound: true);
        m_Interactions_HandRight = m_Interactions.FindAction("HandRight", throwIfNotFound: true);
        m_Interactions_Interaction = m_Interactions.FindAction("Interaction", throwIfNotFound: true);
        // Other
        m_Other = asset.FindActionMap("Other", throwIfNotFound: true);
        m_Other_SkipCutscene = m_Other.FindAction("SkipCutscene", throwIfNotFound: true);
        m_Other_Pause = m_Other.FindAction("Pause", throwIfNotFound: true);
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

    // Interactions
    private readonly InputActionMap m_Interactions;
    private List<IInteractionsActions> m_InteractionsActionsCallbackInterfaces = new List<IInteractionsActions>();
    private readonly InputAction m_Interactions_HandLeft;
    private readonly InputAction m_Interactions_HandRight;
    private readonly InputAction m_Interactions_Interaction;
    public struct InteractionsActions
    {
        private @Default m_Wrapper;
        public InteractionsActions(@Default wrapper) { m_Wrapper = wrapper; }
        public InputAction @HandLeft => m_Wrapper.m_Interactions_HandLeft;
        public InputAction @HandRight => m_Wrapper.m_Interactions_HandRight;
        public InputAction @Interaction => m_Wrapper.m_Interactions_Interaction;
        public InputActionMap Get() { return m_Wrapper.m_Interactions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionsActions set) { return set.Get(); }
        public void AddCallbacks(IInteractionsActions instance)
        {
            if (instance == null || m_Wrapper.m_InteractionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InteractionsActionsCallbackInterfaces.Add(instance);
            @HandLeft.started += instance.OnHandLeft;
            @HandLeft.performed += instance.OnHandLeft;
            @HandLeft.canceled += instance.OnHandLeft;
            @HandRight.started += instance.OnHandRight;
            @HandRight.performed += instance.OnHandRight;
            @HandRight.canceled += instance.OnHandRight;
            @Interaction.started += instance.OnInteraction;
            @Interaction.performed += instance.OnInteraction;
            @Interaction.canceled += instance.OnInteraction;
        }

        private void UnregisterCallbacks(IInteractionsActions instance)
        {
            @HandLeft.started -= instance.OnHandLeft;
            @HandLeft.performed -= instance.OnHandLeft;
            @HandLeft.canceled -= instance.OnHandLeft;
            @HandRight.started -= instance.OnHandRight;
            @HandRight.performed -= instance.OnHandRight;
            @HandRight.canceled -= instance.OnHandRight;
            @Interaction.started -= instance.OnInteraction;
            @Interaction.performed -= instance.OnInteraction;
            @Interaction.canceled -= instance.OnInteraction;
        }

        public void RemoveCallbacks(IInteractionsActions instance)
        {
            if (m_Wrapper.m_InteractionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInteractionsActions instance)
        {
            foreach (var item in m_Wrapper.m_InteractionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InteractionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InteractionsActions @Interactions => new InteractionsActions(this);

    // Other
    private readonly InputActionMap m_Other;
    private List<IOtherActions> m_OtherActionsCallbackInterfaces = new List<IOtherActions>();
    private readonly InputAction m_Other_SkipCutscene;
    private readonly InputAction m_Other_Pause;
    public struct OtherActions
    {
        private @Default m_Wrapper;
        public OtherActions(@Default wrapper) { m_Wrapper = wrapper; }
        public InputAction @SkipCutscene => m_Wrapper.m_Other_SkipCutscene;
        public InputAction @Pause => m_Wrapper.m_Other_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Other; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OtherActions set) { return set.Get(); }
        public void AddCallbacks(IOtherActions instance)
        {
            if (instance == null || m_Wrapper.m_OtherActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_OtherActionsCallbackInterfaces.Add(instance);
            @SkipCutscene.started += instance.OnSkipCutscene;
            @SkipCutscene.performed += instance.OnSkipCutscene;
            @SkipCutscene.canceled += instance.OnSkipCutscene;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(IOtherActions instance)
        {
            @SkipCutscene.started -= instance.OnSkipCutscene;
            @SkipCutscene.performed -= instance.OnSkipCutscene;
            @SkipCutscene.canceled -= instance.OnSkipCutscene;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
        }

        public void RemoveCallbacks(IOtherActions instance)
        {
            if (m_Wrapper.m_OtherActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IOtherActions instance)
        {
            foreach (var item in m_Wrapper.m_OtherActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_OtherActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public OtherActions @Other => new OtherActions(this);
    public interface IInteractionsActions
    {
        void OnHandLeft(InputAction.CallbackContext context);
        void OnHandRight(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
    }
    public interface IOtherActions
    {
        void OnSkipCutscene(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
