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
    Ending9
}

public static class Events
{
    private static int newlyAchievedEnding = -1;

    private static ulong eventsCompleted; //A bitfield for storing
    public static void SetEventComplete(GameEvent gameEvent)
    {
        eventsCompleted |= (ulong)1 << (int)gameEvent;
        SaveToFile();
    }
    public static void SetEventsComplete(ulong events)
    {
        eventsCompleted = events;
        SaveToFile();
    }

    public static void SetEventIncomplete(GameEvent gameEvent)
    {
        eventsCompleted &= ~((ulong)1 << (int)gameEvent);
    }

    public static bool GetEventComplete(GameEvent gameEvent) => (eventsCompleted & (ulong)1 << (int)gameEvent) != 0;

    public static string eventsFileLocation => @$"{Application.persistentDataPath}\events.butt";

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