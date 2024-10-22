using UnityEngine;

public class CassettePlayer : MonoBehaviour, IInteractable, IResetStatic {


    //setup variables for the casset player game object that allows it to play the cassette ending on interaction with casset object
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
