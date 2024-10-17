using UnityEngine;

public class Key : PickupableObject {
    public bool Held { get; private set; } = false;

    public override void PickUp(ref PickupableObject hand)
    {
        base.PickUp(ref hand);
        Held = true;
    }

    public override void Throw(ref PickupableObject hand, Vector3 force)
    {
        base.Throw(ref hand, force);
        Held = false;
    }
}
