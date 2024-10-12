using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour, IGameState
{
    private void Awake()
    {
        GameManager.Subscribe(this);
    }

    public void OnGameStateChanged(GameState gameState)
    {
        
    }

    public void cutsceneEnd(int ending)
    {
        Events.SetEventComplete((GameEvent)ending);

        SceneManager.LoadScene(0);
    }
}
