using UnityEngine;

public class Scroll : PickupableObject {
    public override void PickUp(ref PickupableObject hand)
    {
        base.PickUp(ref hand);
        JustinAIScript.scrollIsHeld = true;
    }

    public override void Throw(ref PickupableObject hand, Vector3 force)
    {
        base.Throw(ref hand, force);
        JustinAIScript.scrollIsHeld = false;
    }
}
