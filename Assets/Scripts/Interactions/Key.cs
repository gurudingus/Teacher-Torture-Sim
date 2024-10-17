using UnityEngine;

public class Key : PickupableObject {

    public override void PickUp(ref PickupableObject hand)
    {
        base.PickUp(ref hand);
    }

    public override void Throw(ref PickupableObject hand, Vector3 force)
    {
        base.Throw(ref hand, force);
    }
}
