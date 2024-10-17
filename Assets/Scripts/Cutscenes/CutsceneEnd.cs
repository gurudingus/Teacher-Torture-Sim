using UnityEngine;

public class CutsceneEnd : MonoBehaviour {

    public void ReloadLevel() => GameManager.LoadLevel(0);
    public void loadCredits() => GameManager.LoadLevel(1);
}