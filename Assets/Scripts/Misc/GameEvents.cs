using System.IO;
using UnityEngine;

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

public static class Events
{
    private static ulong eventsCompleted; //A bitfield for storing
    public static void SetEventComplete(GameEvents gameEvent) => eventsCompleted |= (ulong)1 << (int)gameEvent;
    public static void SetEventIncomplete(GameEvents gameEvent) => eventsCompleted &= ~((ulong)1 << (int)gameEvent);
    public static bool GetEventComplete(GameEvents gameEvent) => (eventsCompleted & (ulong)1 << (int)gameEvent) != 0;

    private static readonly string eventsFileLocation = @$"{Application.persistentDataPath}/events.butt";

    public static void IOTest()
    {
        eventsCompleted = ulong.MaxValue - 234523542345;
        Debug.Log(eventsCompleted);
        SaveToFile();
        LoadFromFile();
        Debug.Log(eventsCompleted);

        Debug.Log(eventsFileLocation);
    }

    public static void LoadFromFile()
    {
        eventsCompleted = 0;

        if (!File.Exists(eventsFileLocation)) return;

        FileStream stream = File.Create(Application.persistentDataPath);
        if (stream.Length != 8)
        {
            stream.Close();
            return;
        }

        BinaryReader reader = new(stream);

        eventsCompleted = (ulong)reader.Read();

        reader.Close();
        stream.Close();
    }

    public static void SaveToFile()
    {
        FileStream stream = File.Open(Application.persistentDataPath, FileMode.Open);
        if (stream.Length != 8) return;

        BinaryWriter writer = new(stream);

        writer.Write(eventsCompleted);

        writer.Close();
        stream.Close();
    }
}