using UnityEngine;
using UnityEngine.InputSystem;
//Liam Script
using PUO = PickupableObject; //This makes PickupableObject slightly less verbose

[RequireComponent(typeof(PlayerInput))] public class PickUpObject : MonoBehaviour, IGameState
{
    //Serialised Fields
    //[SerializeField] private float singleHandMaximumMass = 10f;

    [SerializeField][Tooltip("The positon and rotation of the left hand (Black ball)")] private PositionRotation leftHandTransform = new(new Vector3(-0.5f, 0.5f, 1f), Quaternion.Euler(0f, 0f, 15f));
    [SerializeField][Tooltip("The positon and rotation of the right hand (White ball)")] private PositionRotation rightHandTransform = new(new Vector3(0.5f, 0.5f, 1f), Quaternion.Euler(0f, 0f, -15f));
    //private PositionRotation middleHandTransform; //The position that objects will be in when they are held by both hands at once
    [SerializeField] private Vector3 force = Vector3.forward * 10f;
    private Vector3 Force => camera.transform.rotation * force;
    [SerializeField] private float holdTimeForMaxForce = 0.75f;

    //Fields
    public static PUO leftHand = null;
    public static PUO rightHand = null;
    //private bool isStrongHold => leftHand == rightHand && leftHand != null; //Returns true if both hands are holding the same item and are not null

    private CrosshairCast crosshair;

    private new Camera camera;

    private void Awake()
    {
        GameManager.Subscribe(this);

        camera = Camera.main;
        crosshair = GameObject.Find("Crosshair")?.GetComponent<CrosshairCast>();
        //middleHandTransform = leftHandTransform | rightHandTransform;
    }
    
    void Update()
    {
        //Todo: Make sure that double hand holding works correctly / checking which object is in the other hand to make sure you can't half pick up 2 heavy objects -> line ~45 for pick up handling, here for position handling
        leftHand?.SetPosition(leftHandTransform - Vector3.forward * Mathf.SmoothStep(0f, 0.5f, HoldStrength(leftHand.Hold)), transform); //Currently only have basic single hand holding done with only basic null handling (bugger it it works)
        rightHand?.SetPosition(rightHandTransform - Vector3.forward * Mathf.SmoothStep(0f, 0.5f, HoldStrength(rightHand.Hold)), transform);
    }

    private void OnHandLeft(InputValue input) => OnHand(ref leftHand, input.isPressed); //Run OnHand with a reference to the left hand
    private void OnHandRight(InputValue input) => OnHand(ref rightHand, input.isPressed); //Run OnHand with a reference to the right hand

    private void OnHand(ref PUO chosenHand, bool pressed)
    {
        if (pressed) //If the button was pressed down, pick up an object or start the timer on a held object
        {
            if (chosenHand == null) crosshair.pickupableObject?.PickUp(ref chosenHand); //Pick up if the hand is empty
            else chosenHand?.throwHoldTime.Start(); //Start the hold timer if the hand is not empty

            return;
        }

        if (chosenHand != null && chosenHand.Hold > 0f) chosenHand.Throw(ref chosenHand, Force * HoldStrength(chosenHand.Hold)); //If the button was released, the respective hand is full and it wasn't just picked up, throw it
    }

    private float HoldStrength(float holdTime) => Mathf.Min(1f, holdTime / holdTimeForMaxForce);

    private void OnDrawGizmosSelected() //Some more gizmos to help visualise the position and rotation of item pickup
    {
        PickupUtilities.DrawGizmos(leftHandTransform.GetPosition(transform), leftHandTransform.GetRotation(transform), Color.black);
        PickupUtilities.DrawGizmos(rightHandTransform.GetPosition(transform), rightHandTransform.GetRotation(transform), Color.white);
    }

    public void OnGameStateChanged(GameState gameState) //Drop items in both hands whenever a cutscene starts
    {
        if (gameState != GameState.Cutscene) return;
        leftHand?.Throw(ref leftHand, Vector3.zero);
        rightHand?.Throw(ref rightHand, Vector3.zero);
    }
}