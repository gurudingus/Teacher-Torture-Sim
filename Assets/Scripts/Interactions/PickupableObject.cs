using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    [SerializeField] [Tooltip("Weight in kg")] private float mass = 1;
    public static float Mass(PickupableObject pickupableObject) => pickupableObject == null ? 0 : pickupableObject.mass;

    [SerializeField] [Tooltip("The positon and rotation that will attempt to match whatever is defined on a pickup script")] private PositionRotation pickupTransform;

    public void SetPosition(PositionRotation positionRotation, Transform _transform)
    {
        transform.position = positionRotation.GetPosition(_transform) - pickupTransform.Position;
        transform.rotation = positionRotation.GetRotation(_transform) * Quaternion.Inverse(pickupTransform.Rotation);
    }

    private void OnDrawGizmosSelected()
    {
        PickupUtilities.DrawGizmos(pickupTransform.GetPosition(transform), pickupTransform.GetRotation(transform));
    }
}