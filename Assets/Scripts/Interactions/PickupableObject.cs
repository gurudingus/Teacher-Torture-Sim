using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;

[RequireComponent(typeof(Rigidbody))] public class PickupableObject : MonoBehaviour
{
    [SerializeField] [Tooltip("Weight in kg")] private float mass = 1f;
    public float Mass => mass; 

    public Stopwatch throwHoldTime { get; } = new();
    public float Hold => (float)throwHoldTime.Elapsed.TotalSeconds;

    [SerializeField] [Tooltip("The positon and rotation that will attempt to match whatever is defined on a pickup script")] private PositionRotation pickupTransform;

    private new Rigidbody rigidbody;

    [SerializeField] [Range(0, 0.25f)] [Tooltip("0 means instant snapping / no smoothing, max value means super sluggish movement")] private float smoothDampTime = 0.05f;
    private Vector3 smoothDampVelocity = Vector3.zero;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.mass = mass;
        gameObject.layer = 3; //Makes sure the object is set to the Physics Object layer
    }

    public void SetPosition(PositionRotation positionRotation, Transform _transform)
    {
        transform.position = Vector3.SmoothDamp(transform.position, positionRotation.GetPosition(_transform) - (Vector3)(transform.localToWorldMatrix * pickupTransform.Position), ref smoothDampVelocity, smoothDampTime); //Some funky maths to make the positions match
        transform.rotation = positionRotation.GetRotation(_transform) * Quaternion.Inverse(pickupTransform.Rotation); //Some slightly less funky maths to make the rotations match
    }

    public virtual void PickUp(ref PickupableObject hand)
    {
        hand = this; //Set the hand to be equal to this object
        rigidbody.isKinematic = true; //Make this kinematic so that it is under the control of the PickupObject script
        gameObject.layer = 7; //Set it to the Picked Up Object layer that doesn't intersect with the player
    }

    public virtual void Throw(ref PickupableObject hand, Vector3 force)
    {
        hand = null; //Empty the hand
        rigidbody.isKinematic = false; //Back to physics control
        rigidbody.AddForce(force, ForceMode.Impulse); //Launch the sucker
        gameObject.layer = 3; //Set it back to the Physics Object layer

        throwHoldTime.Stop(); //Stop and reset the timer
        throwHoldTime.Reset();
    }

    private void OnDrawGizmosSelected() => PickupUtilities.DrawGizmos(pickupTransform.GetPosition(transform), pickupTransform.GetRotation(transform)); //Shows the anchor that is used for the pickup
}