using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Written by Liam unless otherwise indicated

/// <summary>
/// Anything that needs to have something happen when the gameState is changed can implement this interface
/// </summary>
public interface IGameEventSubscriber
{
    public abstract void OnGameEvent(GameState gameState);
}

public class GameManager : MonoBehaviour, IResetStatic
{
    private static ulong eventsCompleted;

    public static void SetEventComplete(GameEvents gameEvent)
    {
        eventsCompleted |= (ulong)1 << (int)gameEvent;
    }

    public static void SetEventIncomplete(GameEvents gameEvent)
    {
        eventsCompleted &= ~((ulong)1 << (int)gameEvent);
    }

    public static bool GetEventComplete(GameEvents gameEvent)
    {
        return (eventsCompleted & (ulong)1 << (int)gameEvent) != 0;
    }

    private static List<IGameEventSubscriber> subscribers; //A list of all objects implementing IGameEventSubscriber that will have their OnGameEvent() function called whenever gameState is changed

    public static GameState gameState { get; private set; }

    private void Awake()
    {
        StaticReset.Subscribe(this);
    }

    /// <summary>
    /// Use this to set the game state and automatically call OnGameEvent() on all subscribed objects
    /// </summary>
    /// <param name="state"></param>
    public void SetGameState(GameState state)
    {
        gameState = state;
        foreach (IGameEventSubscriber subscriber in subscribers)
        {
            subscriber.OnGameEvent(state);
        }
    }

    public static void Subscribe(IGameEventSubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    public static void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        StaticReset.ResetStatics();
    }

    public void OnStaticReset()
    {
        gameState = GameState.Menu;
    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused
}

public enum GameEvents
{
    Ending1,
    Ending2,
    Ending3,
    Ending4,
    Ending5,
    Ending6,
    Ending7,
    Ending8,
    Ending9
}