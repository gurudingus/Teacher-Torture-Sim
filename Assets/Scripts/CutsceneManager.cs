using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour, IGameEvent
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Awake()
    {
        GameManager.Subscribe(this);
    }

    public void OnGameEvent(GameState gameState)
    {
        
    }

    public void cutsceneEnd()
    {
        Events.SetEventComplete(GameEvent.Ending1);
        SceneManager.LoadScene(0);
    }
}
