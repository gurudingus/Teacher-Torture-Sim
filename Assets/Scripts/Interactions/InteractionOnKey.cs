using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public abstract void Interact();
}

[RequireComponent(typeof(PlayerInput))] public class InteractionOnKey : MonoBehaviour
{
    private CrosshairCast crosshair;

    private void Awake() => crosshair = GameObject.Find("Crosshair")?.GetComponent<CrosshairCast>();

    private void OnInteraction() => crosshair?.interactable?.Interact(); //? operator to reduce verbosity. If it has an IInteractable implementing object, interact with it.
}