using UnityEngine;

public class ShutdownButton : MonoBehaviour, IInteractable
{
    //Liam Script
    //enables ending four when in game button object is pressed
    public void Interact()
    {
        transform.localPosition += transform.up * 0.05f;

        Invoke(nameof(RunCutscene), 0.2f);
    }

    private void RunCutscene() => CutsceneManager.PlayCutscene(GameEvent.Ending4);
}
