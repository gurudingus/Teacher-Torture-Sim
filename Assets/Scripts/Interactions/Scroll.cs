using UnityEngine;
//Liam Script
public class Scroll : PickupableObject {
    //allows it to interact with justin object when the E key is used.

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
