using UnityEngine;

public class CassettePlayer : MonoBehaviour, IInteractable, IResetStatic {
    public static bool CanInteract { set; private get; } = false;

    private AudioSource speaker;

    private void Awake() => speaker = GetComponent<AudioSource>();

    public void Interact()
    {
        if (CanInteract) CutsceneManager.PlayCutscene(GameEvent.Ending5);
        else speaker.Play();
    }

    public void OnResetStatic() => CanInteract = false;
}
