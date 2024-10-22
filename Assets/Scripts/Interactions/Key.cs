using UnityEngine;
//Liam Script
public class Key : PickupableObject {
    //enables pickup of Key game object, and allows it to interact with scroll of power object when the E key is used.
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
