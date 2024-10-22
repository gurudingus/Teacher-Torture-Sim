using UnityEngine;

public class CassetteTape : PickupableObject {

    //allows it to interact with cassete player object when it is thrown.
    public override void PickUp(ref PickupableObject hand)
    {
        base.PickUp(ref hand);
        CassettePlayer.CanInteract = true;
    }

    public override void Throw(ref PickupableObject hand, Vector3 force)
    {
        base.Throw(ref hand, force);
        CassettePlayer.CanInteract = false;
    }
}
