using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour, IGameEvent
{
    private void Awake()
    {
        GameManager.Subscribe(this);
    }

    public void OnGameEvent(GameState gameState)
    {
        
    }

    public void cutsceneEnd(int ending)
    {

        Events.SetEventComplete((GameEvent)ending);

        SceneManager.LoadScene(0);
    }
}
