using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))] public class PickUpObject : MonoBehaviour
{
    private PickupableObject objectLeftHand;
    private PickupableObject objectRightHand;

    [SerializeField] private float singleHandMaximumMass = 10;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnLeftHand()
    {

    }

    private void OnRightHand()
    {
        
    }
}
