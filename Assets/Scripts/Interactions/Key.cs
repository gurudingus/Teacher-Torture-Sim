using UnityEngine;

public class Key : PickupableObject {
    [SerializeField] private KeyColour key = KeyColour.Purple;

    public override void PickUp(ref PickupableObject hand)
    {
        base.PickUp(ref hand);
        if (key == KeyColour.Purple) Scroll.PurpleKey = true;
        else if (key == KeyColour.Grey) Scroll.GreyKey = true;
    }

    public override void Throw(ref PickupableObject hand, Vector3 force)
    {
        base.Throw(ref hand, force);
        if (key == KeyColour.Purple) Scroll.PurpleKey = false;
        else if (key == KeyColour.Grey) Scroll.GreyKey = false;
    }
}

public enum KeyColour
    {
        Purple,
        Grey,
    }
