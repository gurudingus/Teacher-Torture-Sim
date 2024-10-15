using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;

    private void OnPause()
    {
        GameState previousState = GameManager.gameState;

        bool paused = GameManager.gameState == GameState.Paused;

        GameManager.gameState = paused ? previousState : GameState.Paused; //Update the gamestate to be either paused or not paused
        paused = !paused; //Update paused as well so the next code is more readable
        
        Time.timeScale = paused ? 0f : 1f;
    }
}
