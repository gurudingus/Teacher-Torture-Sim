using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))] public class PickUpObject : MonoBehaviour
{
    private PickupableObject objectLeftHand;
    private PickupableObject objectRightHand;

    new private Camera camera;

    [SerializeField] private float singleHandMaximumMass = 10;

    private void Awake()
    {
        camera = Camera.main;
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

    private bool CheckHandRay(out PickupableObject obj)
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit handRaycast))
        {
            obj = null;
            return false;
        }

        PickupableObject obj2 = handRaycast.transform.gameObject.GetComponent<PickupableObject>();
        if (obj2 != null)
        {
            obj = obj2;
            return true;
        }

        obj = null;
        return false;
    }
}
