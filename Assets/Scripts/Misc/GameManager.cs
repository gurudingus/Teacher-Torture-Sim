using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Written by Liam unless otherwise indicated

/// <summary>
/// Anything that needs to have something happen when the gameState is changed can implement this interface
/// </summary>
public interface IGameState
{
    public abstract void OnGameStateChanged(GameState gameState);
}

public class GameManager : MonoBehaviour, IResetStatic
{
    //Game events
    private static List<IGameState> subscribers = new(); //A list of all objects implementing IGameEventSubscriber that will have their OnGameEvent() function called whenever gameState is changed

    private static GameState state = GameState.Menu;
    public static GameState gameState
    {
        get => state;
        set
        {
            state = value;
            foreach (IGameState subscriber in subscribers) subscriber.OnGameStateChanged(value);
        }
    }

    private void Awake()
    {
        StaticReset.Subscribe(this);
        Events.LoadFromFile();
    }

    private void Start() => gameState = GameState.MenuRoom;

    public static void Subscribe(IGameState subscriber) => subscribers.Add(subscriber);

    public static void LoadLevel(string sceneName)
    {
        StaticReset.ResetStatics(); //This probably should happen before loading a new level, as otherwise anything that does get set on level load would be resets
        SceneManager.LoadScene(sceneName);
    }
    public static void LoadLevel(int sceneID)
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        StaticReset.ResetStatics(); //This probably should happen before loading a new level, as otherwise anything that does get set on level load would be resets
        SceneManager.LoadScene(sceneID);
    }

    public void OnResetStatic() 
    {
        state = GameState.Menu;
        subscribers.Clear();
    }
}

public enum GameState
{
    Menu,
    MenuRoom,
    Playing,
    Paused,
    Cutscene,
    Computer,
}