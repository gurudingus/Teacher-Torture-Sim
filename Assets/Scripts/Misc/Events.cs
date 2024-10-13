using System.IO;
using UnityEngine;

public enum GameEvent
{
    Ending1,
    Ending2,
    Ending3,
    Ending4,
    Ending5,
    Ending6,
    Ending7,
    Ending8,
    Ending9,

    None = int.MaxValue,
}

public static class Events
{
    private static ulong eventsCompleted; //A bitfield for storing

    public static GameEvent mostRecentEvent { get; set; } = GameEvent.None;

    public static void SetEventComplete(GameEvent gameEvent)
    {
        if (GetEventComplete(gameEvent)) return;

        eventsCompleted |= (ulong)1 << (int)gameEvent;
        mostRecentEvent = gameEvent;
        SaveToFile();
    }
    public static void SetEventsComplete(ulong events)
    {
        eventsCompleted = events;
        SaveToFile();
    }

    public static void SetEventIncomplete(GameEvent gameEvent) => eventsCompleted &= ~((ulong)1 << (int)gameEvent);

    public static bool GetEventComplete(GameEvent gameEvent) => (eventsCompleted & (ulong)1 << (int)gameEvent) != 0;
    public static bool GetEventComplete(int gameEvent) => (eventsCompleted & (ulong)1 << gameEvent) != 0;

    public static string eventsFileLocation => @$"{Application.persistentDataPath}\events.ass";

    public static void SaveToFile()
    {
        FileStream stream = File.Create(eventsFileLocation);
        BinaryWriter writer = new(stream);

        writer.Write(eventsCompleted);

        writer.Close();
        stream.Close();
    }

    public static void LoadFromFile()
    {
        eventsCompleted = 0;

        if (!File.Exists(eventsFileLocation)) return;

        FileStream stream = File.Open(eventsFileLocation, FileMode.Open);
        if (stream.Length != 8)
        {
            stream.Close();
            return;
        }

        BinaryReader reader = new(stream);

        eventsCompleted = reader.ReadUInt64();

        reader.Close();
        stream.Close();
    }
}