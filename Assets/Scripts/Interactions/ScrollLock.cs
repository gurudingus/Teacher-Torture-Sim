using UnityEngine;

public class KeysUnlocked
{
    public bool Purple { get; set; } = false;
    public bool Grey { get; set; } = false;
    public bool Unlocked { get => Purple && Grey; }
}

public class ScrollLock : MonoBehaviour, IInteractable {
    [SerializeField] Key purpleKey;
    [SerializeField] Key greyKey;

    private static KeysUnlocked unlocks = new();

    private AudioSource speaker;

    private void Awake() => speaker = transform.parent.GetComponent<AudioSource>();

    public void Interact()
    {
        speaker.Play();
        if (purpleKey.Held) unlocks.Purple = true;
        if (greyKey.Held) unlocks.Grey = true;
        
        if (unlocks.Unlocked) Unlock();
    }

    private void Unlock()
    {
        Debug.Log("Unlocked");
    }
}