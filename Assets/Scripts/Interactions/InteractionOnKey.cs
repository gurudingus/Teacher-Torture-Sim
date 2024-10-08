using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public abstract void Interact();
}

[RequireComponent(typeof(PlayerInput))] public class InteractionOnKey : MonoBehaviour
{
    [SerializeField] private Transform interactorSource;
    [SerializeField] [Tooltip("Overrides the interactor source to automatically select the camera attached to this gameobject")] private bool useCameraAsSource = true;
    [SerializeField] private float interactionRange = 2.5f;

    private void Awake()
    {
        if (useCameraAsSource) interactorSource = GetComponentInChildren<Camera>()?.transform;
    }

    private void OnInteraction()
    {
        if (!Physics.SphereCast(interactorSource.position, 0.1f, interactorSource.forward, out RaycastHit rayHit, interactionRange, ~(1 << 6))) return; //Return if it hits nothing. Also has a layermask exclusion so that it doesn't pick up the player and cancel interactions

        rayHit.transform.gameObject.GetComponentInChildren<IInteractable>()?.Interact(); //? operator to reduce verbosity. If it has an IInteractable implementing object, interact with it.
    }
}
