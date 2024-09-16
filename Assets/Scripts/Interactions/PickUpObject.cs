using UnityEngine;
using UnityEngine.InputSystem;

using PUO = PickupableObject; //This makes PickupableObject.Mass() slightly less verbose

[RequireComponent(typeof(PlayerInput))] public class PickUpObject : MonoBehaviour
{
    //Serialised Fields
    [SerializeField] private float singleHandMaximumMass = 10f;
    [SerializeField] private float maximumRange = 2f;

    [SerializeField] [Tooltip("The positon and rotation of the left hand (Black ball)")] private PositionRotation leftHandTransform = new(-0.5f, 0f, 0.5f);
    [SerializeField] [Tooltip("The positon and rotation of the right hand (White ball)")] private PositionRotation rightHandTransform = new(0.5f, 0f, 0.5f);
    private PositionRotation middleHandTransform; //The position that objects will be in when they are 

    //Fields
    private PUO objectLeftHand;
    private PUO objectRightHand;
    private bool isStrongHold => objectLeftHand == objectRightHand && objectLeftHand != null; //Returns true if both hands are holding the same item and are not null

    new private Camera camera;

    private void Awake()
    {
        camera = Camera.main;

        middleHandTransform = leftHandTransform | rightHandTransform;
    }

    void Update()
    {
        objectLeftHand.SetPosition(leftHandTransform, transform);
    }

    private void OnPickupItem(InputValue input)
    {
        if (CheckHandRay(out PUO pickupableObject))
        {
            if (PUO.Mass(pickupableObject) > singleHandMaximumMass)
            {
                //TODO - Handle double handed pickup logic here, for now is just an early return
                Debug.Log("Object was too heavy");
                return;
            }

            switch (input.Get<float>()) {
                case -1: //Gross floating point comparisons that do thankfully work properly
                    objectLeftHand = pickupableObject;
                    break;
                case 1:
                    objectRightHand = pickupableObject;
                    break;
            }
        }

        Debug.Log($"Left: {PUO.Mass(objectLeftHand)}");
        Debug.Log($"Right: {PUO.Mass(objectRightHand)}");
    }

    private bool CheckHandRay(out PUO obj)
    {
        if (!Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit handRaycast, maximumRange)) //TODO - Use a layer system for objects that can be picked up
        { //Early return if the raycast hits nothing
            obj = null;
            return false;
        }

        PUO pickupableObject = handRaycast.transform.gameObject.GetComponent<PUO>();

        if (pickupableObject == null)
        { //Early return if the hit object has no PickupableObject component. Is likely unnecessary if I end up implementing a layer for it
            obj = null;
            return false;
        }

        obj = pickupableObject;
        return true;
    }

    private void OnDrawGizmosSelected()
    { //Some more gizmos to help visualise the position and rotation of item pickup
        PickupUtilities.DrawGizmos(leftHandTransform.GetPosition(transform), leftHandTransform.GetRotation(transform), Color.black);
        PickupUtilities.DrawGizmos(rightHandTransform.GetPosition(transform), rightHandTransform.GetRotation(transform), Color.white);
    }
}