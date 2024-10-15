using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;

    private GameState previousState;

    private void Awake()
    {
        pauseMenu = GameObject.Find("Pause Menu");
        if (pauseMenu.IsUnityNull()) Debug.LogWarning("Pause menu not found");
        pauseMenu.SetActive(false);
    }

    private void OnPause()
    {
        if (GameManager.gameState != GameState.Paused) previousState = GameManager.gameState;

        bool playing = GameManager.gameState == GameState.Paused; //Inverted here so it makes sense for the rest of the code

        GameManager.gameState = playing ? previousState : GameState.Paused; //Update the gamestate to be either paused or not paused
        
        Time.timeScale = playing ? 1f : 0f;
        Cursor.lockState = playing ? CursorLockMode.Locked : CursorLockMode.None;
        pauseMenu.SetActive(!playing);
    }
}
