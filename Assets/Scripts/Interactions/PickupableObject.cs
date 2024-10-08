using UnityEngine;

[RequireComponent(typeof(Rigidbody))] public class PickupableObject : MonoBehaviour
{
    [SerializeField] [Tooltip("Weight in kg")] private float mass = 1;
    public float Mass => mass; 

    [SerializeField] [Tooltip("The positon and rotation that will attempt to match whatever is defined on a pickup script")] private PositionRotation pickupTransform;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.mass = mass;
        gameObject.layer = 3;
    }

    public void SetPosition(PositionRotation positionRotation, Transform _transform)
    {
        transform.position = positionRotation.GetPosition(_transform) - (Vector3)(transform.localToWorldMatrix * pickupTransform.Position); //Some funky maths to make the positions match
        transform.rotation = positionRotation.GetRotation(_transform) * Quaternion.Inverse(pickupTransform.Rotation); //Some slightly less funky maths to make the rotations match
    }

    public void PickUp(ref PickupableObject hand)
    {
        hand = this; //Set the hand to be equal to this object
        rigidbody.isKinematic = true; //Make this kinematic so that it is under the control of the PickupObject script
        gameObject.layer = 7;
    }

    public void Throw(ref PickupableObject hand, Vector3 force)
    {
        hand = null; //Empty the hand
        rigidbody.isKinematic = false; //Back to physics control
        rigidbody.AddForce(force, ForceMode.Impulse); //Launch the sucker
        gameObject.layer = 3;
    }

    private void OnDrawGizmosSelected() => PickupUtilities.DrawGizmos(pickupTransform.GetPosition(transform), pickupTransform.GetRotation(transform)); //Shows the anchor that is used for the pickup
}