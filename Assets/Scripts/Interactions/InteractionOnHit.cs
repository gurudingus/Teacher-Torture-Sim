using UnityEngine;
//Liam Script
public class InteractionOnHit : MonoBehaviour
{
    //believe it or not, this enables interaction...On hit!
    private void OnCollisionEnter(Collision other) => other.gameObject.GetComponentInChildren<IInteractable>()?.Interact();
}
