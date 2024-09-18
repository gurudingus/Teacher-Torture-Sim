using UnityEngine;

[RequireComponent(typeof(Rigidbody))] public class PickupableObject : MonoBehaviour
{
    [SerializeField] [Tooltip("Weight in kg")] private float mass = 1;
    public float Mass => mass; 

    [SerializeField] [Tooltip("The positon and rotation that will attempt to match whatever is defined on a pickup script")] private PositionRotation pickupTransform;

    private new Rigidbody rigidbody;

    private void Awake() => rigidbody = GetComponent<Rigidbody>();

    public void SetPosition(PositionRotation positionRotation, Transform _transform)
    {
        Vector3 positionOtherAnchor = positionRotation.GetPosition(_transform);
        Vector3 vectorOriginToAnchor = pickupTransform.GetPosition(transform) - transform.position;

        transform.position = positionOtherAnchor - vectorOriginToAnchor;
        transform.rotation = positionRotation.GetRotation(_transform) * Quaternion.Inverse(pickupTransform.Rotation);
    }

    public bool PickUp(ref PickupableObject hand) //Bool return for switch hacking
    {
        hand = this;
        rigidbody.isKinematic = true;

        return true;
    }

    public bool Throw(ref PickupableObject hand, Vector3 force) //Same thing with the bool return
    {
        hand = null;
        rigidbody.isKinematic = false;
        rigidbody.AddForce(force, ForceMode.Impulse);

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        PickupUtilities.DrawGizmos(pickupTransform.GetPosition(transform), pickupTransform.GetRotation(transform));
    }
}