using UnityEngine;
using UnityEngine.InputSystem;

using PUO = PickupableObject; //This makes PickupableObject.Mass() slightly less verbose

[RequireComponent(typeof(PlayerInput))] public class PickUpObject : MonoBehaviour
{
    //Serialised Fields
    [SerializeField] private float singleHandMaximumMass = 10f;
    [SerializeField] private float maximumRange = 2f;

    [SerializeField] [Tooltip("The positon and rotation of the left hand (Black ball)")] private PositionRotation leftHandTransform = new(new Vector3(-0.5f, 0.5f, 1f), Quaternion.Euler(0f, 0f, 15f));
    [SerializeField] [Tooltip("The positon and rotation of the right hand (White ball)")] private PositionRotation rightHandTransform = new(new Vector3(0.5f, 0.5f, 1f), Quaternion.Euler(0f, 0f, -15f));
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
        //Todo: Make sure that double hand holding works correctly / checking which object is in the other hand to make sure you can't half pick up 2 heavy objects -> line ~45 for pick up handling, here for position handling

        objectLeftHand?.SetPosition(leftHandTransform, transform); //Currently only have basic single hand holding done with only basic null handling
        objectRightHand?.SetPosition(rightHandTransform, transform);
    }

    private void OnPickupItem(InputValue input)
    {
        if (!CheckHandRay(out PUO pickupableObject)) return;

        if (pickupableObject?.Mass > singleHandMaximumMass)
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

        Debug.Log($"Left: {objectLeftHand?.Mass}");
        Debug.Log($"Right: {objectRightHand?.Mass}");
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