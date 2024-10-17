using UnityEngine;

public class KeysUnlocked
{
    public bool Purple { get; set; } = false;
    public bool Grey { get; set; } = false;

    public bool Unlocked { get => Purple && Grey; }
}

public class Scroll : MonoBehaviour, IInteractable {

    public static bool PurpleKey { private get; set; } = false;
    public static bool GreyKey { private get; set; } = false;

    private static KeysUnlocked unlocks = new();
    [SerializeField] private KeyColour key = KeyColour.Purple;

    private AudioSource speaker;

    private void Awake() => speaker = transform.parent.GetComponent<AudioSource>();

    public void Interact()
    {
        speaker.Play();
        if ((key == KeyColour.Purple && !PurpleKey) || (key == KeyColour.Grey && !GreyKey)) return;
        else
        {
            if (key == KeyColour.Purple) unlocks.Purple = true;
            else if (key == KeyColour.Grey) unlocks.Grey = true;

            if (unlocks.Unlocked) Unlock();
        }
    }

    private void Unlock()
    {

    }
}
