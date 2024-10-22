using UnityEngine;
//Liam Script
public class KeysUnlocked
{
    private bool purple;
    public void Purple(Key key)
    {
        purple = true;

        DropKey(key);
        GameObject.Destroy(key.gameObject);
    }

    private bool grey;
    public void Grey(Key key)
    {
        grey = true;

        DropKey(key);
        GameObject.Destroy(key.gameObject);
    }

    private void DropKey(Key key)
    {
        if(key == PickUpObject.leftHand) key.Throw(ref PickUpObject.leftHand, Vector3.zero);
        if(key == PickUpObject.rightHand) key.Throw(ref PickUpObject.rightHand, Vector3.zero);
    }

    public bool Unlocked { get => purple && grey; }
}

public class ScrollLock : MonoBehaviour, IInteractable {
    [SerializeField] Key purpleKey;
    [SerializeField] Key greyKey;
    [SerializeField] GameObject openScroll;

    private KeysUnlocked unlocks = new();

    private AudioSource speaker;

    private void Awake() => speaker = transform.parent.GetComponent<AudioSource>();

    public void Interact()
    {
        speaker.Play();
        if (purpleKey.Held) unlocks.Purple(purpleKey);
        if (greyKey.Held) unlocks.Grey(greyKey);
        
        if (unlocks.Unlocked) Unlock();
    }

    private void Unlock()
    {
        Destroy(transform.parent.gameObject);
        Instantiate(openScroll, transform.parent.position, transform.parent.rotation);
    }
}