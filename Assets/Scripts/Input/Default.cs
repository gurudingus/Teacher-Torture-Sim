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
            ""name"": ""Pickup-Throw"",
            ""id"": ""ca65c367-ad1f-41a7-b250-da03a059cae5"",
            ""actions"": [
                {
                    ""name"": ""PickupItem"",
                    ""type"": ""Button"",
                    ""id"": ""54d16d58-8ca3-4e7a-bb38-64c878af91a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e26aafa1-6c85-45ee-9d58-d4c57026b566"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickupItem"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ba430647-badc-4828-9360-51980fb8d9d4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickupItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d59c4c72-9a69-45c4-a78f-60b596c9c4bd"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickupItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Pickup-Throw
        m_PickupThrow = asset.FindActionMap("Pickup-Throw", throwIfNotFound: true);
        m_PickupThrow_PickupItem = m_PickupThrow.FindAction("PickupItem", throwIfNotFound: true);
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

    // Pickup-Throw
    private readonly InputActionMap m_PickupThrow;
    private List<IPickupThrowActions> m_PickupThrowActionsCallbackInterfaces = new List<IPickupThrowActions>();
    private readonly InputAction m_PickupThrow_PickupItem;
    public struct PickupThrowActions
    {
        private @Default m_Wrapper;
        public PickupThrowActions(@Default wrapper) { m_Wrapper = wrapper; }
        public InputAction @PickupItem => m_Wrapper.m_PickupThrow_PickupItem;
        public InputActionMap Get() { return m_Wrapper.m_PickupThrow; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PickupThrowActions set) { return set.Get(); }
        public void AddCallbacks(IPickupThrowActions instance)
        {
            if (instance == null || m_Wrapper.m_PickupThrowActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PickupThrowActionsCallbackInterfaces.Add(instance);
            @PickupItem.started += instance.OnPickupItem;
            @PickupItem.performed += instance.OnPickupItem;
            @PickupItem.canceled += instance.OnPickupItem;
        }

        private void UnregisterCallbacks(IPickupThrowActions instance)
        {
            @PickupItem.started -= instance.OnPickupItem;
            @PickupItem.performed -= instance.OnPickupItem;
            @PickupItem.canceled -= instance.OnPickupItem;
        }

        public void RemoveCallbacks(IPickupThrowActions instance)
        {
            if (m_Wrapper.m_PickupThrowActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPickupThrowActions instance)
        {
            foreach (var item in m_Wrapper.m_PickupThrowActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PickupThrowActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PickupThrowActions @PickupThrow => new PickupThrowActions(this);
    public interface IPickupThrowActions
    {
        void OnPickupItem(InputAction.CallbackContext context);
    }
}
