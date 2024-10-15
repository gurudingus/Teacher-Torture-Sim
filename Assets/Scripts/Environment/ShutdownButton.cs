using UnityEngine;

public class ShutdownButton : MonoBehaviour, IInteractable
{




    public void Interact()
    {
        transform.localPosition -= transform.right * 0.05f;

        Invoke(nameof(RunCutscene), 0.2f);
    }

    private void RunCutscene() => CutsceneManager.PlayCutscene(GameEvent.Ending4);
}
