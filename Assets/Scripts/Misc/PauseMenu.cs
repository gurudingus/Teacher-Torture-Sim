using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameState previousState;
    private GameObject[] children;

    private void Awake() {
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) children[i] = transform.GetChild(i).gameObject;

        foreach (GameObject child in children) child.SetActive(false);
    }

    private void OnPause()
    {
        if (GameManager.gameState != GameState.Paused) previousState = GameManager.gameState;

        bool playing = GameManager.gameState == GameState.Paused; //Inverted here so it makes sense for the rest of the code

        GameManager.gameState = playing ? previousState : GameState.Paused; //Update the gamestate to be either paused or not paused
        
        Time.timeScale = playing ? 1f : 0f;
        Cursor.lockState = playing ? CursorLockMode.Locked : CursorLockMode.None;

        foreach (GameObject child in children) child.SetActive(!playing);
    }

    public void Resume() => OnPause();
    public void ReturnToMenu() {
        GameManager.LoadLevel(0);
    }
    public void QuitGame() => Application.Quit();
}
