using UnityEngine;
using UnityEngine.InputSystem;

using PUO = PickupableObject; //This makes PickupableObject.Mass() slightly less verbose

[RequireComponent(typeof(PlayerInput))] public class PickUpObject : MonoBehaviour
{
    //Serialised Fields
    //[SerializeField] private float singleHandMaximumMass = 10f;
    [SerializeField] private float maximumRange = 2f;

    [SerializeField] [Tooltip("The positon and rotation of the left hand (Black ball)")] private PositionRotation leftHandTransform = new(new Vector3(-0.5f, 0.5f, 1f), Quaternion.Euler(0f, 0f, 15f));
    [SerializeField] [Tooltip("The positon and rotation of the right hand (White ball)")] private PositionRotation rightHandTransform = new(new Vector3(0.5f, 0.5f, 1f), Quaternion.Euler(0f, 0f, -15f));
    private PositionRotation middleHandTransform; //The position that objects will be in when they are held by both hands at once
    [SerializeField] private Vector3 force = Vector3.forward * 10f;
    private Vector3 Force => camera.transform.rotation * force;

    //Fields
    private PUO leftHand = null;
    private PUO rightHand = null;
    //private bool isStrongHold => leftHand == rightHand && leftHand != null; //Returns true if both hands are holding the same item and are not null

    new private Camera camera;

    private void Awake()
    {
        camera = Camera.main;

        middleHandTransform = leftHandTransform | rightHandTransform;
    }

    void Update()
    {
        //Todo: Make sure that double hand holding works correctly / checking which object is in the other hand to make sure you can't half pick up 2 heavy objects -> line ~45 for pick up handling, here for position handling

        leftHand?.SetPosition(leftHandTransform, transform); //Currently only have basic single hand holding done with only basic null handling
        rightHand?.SetPosition(rightHandTransform, transform);
    }

    private void OnPickupItem(InputValue input) //TODO - I should probably refactor this to only use a single switch and then just call another function or use a reference or something
    {
        Hand hand = input.Get<float>() == -1f ? Hand.Left : Hand.Right;

        bool clickedHandHasItem = hand switch //Check whether or not the hand that was interacted with has an item in it
        {
            Hand.Left => leftHand != null,
            Hand.Right => rightHand != null,
            _ => false
        };

        if (clickedHandHasItem) //If the current hand has an item in it, throw it and return from the function
        {
            bool _1 = hand switch
            {
                Hand.Left => leftHand.Throw(ref leftHand, Force),
                Hand.Right => rightHand.Throw(ref rightHand, Force),
                _ => false
            };
            return;
        }

        if (!CheckHandRay(out PUO pickupableObject) & !clickedHandHasItem) return; //If there is no item in the hand, first check if there is an item in front of the camera, getting the object or returning if nothing is in front

        //TODO - Handle double handed pickup logic here, for now is just an early return that I have commented out
        //if (pickupableObject.Mass > singleHandMaximumMass)
        //{
        //    Debug.Log("Object was too heavy");
        //    return;
        //}

        bool _ = hand switch //Pick up an item in the appropriate hand
        {
            Hand.Left => pickupableObject.PickUp(ref leftHand),
            Hand.Right => pickupableObject.PickUp(ref rightHand),
            _ => false
        };
    }

    private bool CheckHandRay(out PUO obj)
    {
        if (!Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit handRaycast, maximumRange)) //TODO - Use a layer system for objects that can be picked up
        { 
            obj = null;
            return false; //Early return if the raycast hits nothing
        }

        PUO pickupableObject = handRaycast.transform.gameObject.GetComponent<PUO>();

        if (pickupableObject == null) 
        {
            obj = null;
            return false; //Early return if the hit object has no PickupableObject component. Is probably unnecessary if I end up implementing a layer for it
        }

        obj = pickupableObject;
        return true;
    }

    private void OnDrawGizmosSelected() //Some more gizmos to help visualise the position and rotation of item pickup
    {
        PickupUtilities.DrawGizmos(leftHandTransform.GetPosition(transform), leftHandTransform.GetRotation(transform), Color.black);
        PickupUtilities.DrawGizmos(rightHandTransform.GetPosition(transform), rightHandTransform.GetRotation(transform), Color.white);
    }

    enum Hand
    {
        Left,
        Right
    }
}