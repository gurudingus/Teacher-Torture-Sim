using UnityEngine;

public class InteractionOnHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) => other.gameObject.GetComponentInChildren<IInteractable>()?.Interact();
}
