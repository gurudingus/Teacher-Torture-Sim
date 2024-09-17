using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    [SerializeField] [Tooltip("Weight in kg")] private float mass = 1;
    public float Mass => mass;

    [SerializeField] [Tooltip("The positon and rotation that will attempt to match whatever is defined on a pickup script")] private PositionRotation pickupTransform;

    public void SetPosition(PositionRotation positionRotation, Transform _transform)
    {
        Vector3 positionOtherAnchor = positionRotation.GetPosition(_transform);
        Vector3 vectorOriginToAnchor = pickupTransform.GetPosition(transform) - transform.position;

        transform.position = positionOtherAnchor - vectorOriginToAnchor;
        transform.rotation = positionRotation.GetRotation(_transform) * Quaternion.Inverse(pickupTransform.Rotation);
    }

    private void OnDrawGizmosSelected()
    {
        PickupUtilities.DrawGizmos(pickupTransform.GetPosition(transform), pickupTransform.GetRotation(transform));
    }
}