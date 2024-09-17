using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Written by Liam unless otherwise indicated

/// <summary>
/// Anything that needs to have something happen when the gameState is changed can implement this interface
/// </summary>
public interface IGameEvent
{
    public abstract void OnGameEvent(GameState gameState);
}

public class GameManager : MonoBehaviour, IResetStatic
{
    //Game events
    private static List<IGameEvent> subscribers = new(); //A list of all objects implementing IGameEventSubscriber that will have their OnGameEvent() function called whenever gameState is changed

    private static GameState state = GameState.Menu;
    public static GameState gameState
    {
        get => state;
        set
        {
            state = value;
            foreach (IGameEvent subscriber in subscribers) subscriber.OnGameEvent(value);
        }
    }

    private void Awake()
    {
        StaticReset.Subscribe(this);
        Events.IOTest();
    }

    public static void Subscribe(IGameEvent subscriber) => subscribers.Add(subscriber);

    public static void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        StaticReset.ResetStatics();
    }

    public void OnStaticReset()
    {
        state = GameState.Menu;
    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused
}