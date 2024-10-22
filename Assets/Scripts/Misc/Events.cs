using System.IO;
using UnityEngine;

using Bitfield64 = System.UInt64; //Nice little class alias (would have used a type alias if not for old C#) to make the code slightly more self explanatory

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
    private static Bitfield64 eventsCompleted = 0; //A bitfield for storing which events are complete

    public static GameEvent mostRecentEvent { get; set; } = GameEvent.None;

    public static void SetEventComplete(GameEvent gameEvent)
    {
        if (GetEventComplete(gameEvent)) return; //Don't do anything if the event is already complete

        eventsCompleted |= (Bitfield64)1 << (int)gameEvent; //Bitwise OR with nothing but a 1 in in the gameEvent-th place
        mostRecentEvent = gameEvent;
        SaveToFile(); //Save to file whenever events are set
    }
    public static void SetEventsComplete(Bitfield64 events)
    {
        eventsCompleted = events; //Blanket set event that takes an entire bitfield in and replaces the events with it
        SaveToFile();
    }

    public static void SetEventIncomplete(GameEvent gameEvent) => eventsCompleted &= ~((Bitfield64)1 << (int)gameEvent); //And with a negative bitfield (all 1s with a 0 in the gameEvent-th)

    public static bool GetEventComplete(GameEvent gameEvent) => (eventsCompleted & (Bitfield64)1 << (int)gameEvent) != 0; //Gets the bit and the position
    public static bool GetEventComplete(int gameEvent) => (eventsCompleted & (Bitfield64)1 << gameEvent) != 0; //Gets the bit at the position but int. No idea why I made this override

    public static string eventsFileLocation => @$"{Application.persistentDataPath}\events.ass.justintime.fart3.craggles.frog.CastleDoore.edge"; //Fantastic filename with contributions from everyone in the class

    public static void SaveToFile()
    {
        FileStream stream = File.Create(eventsFileLocation);
        BinaryWriter writer = new(stream);

        writer.Write(eventsCompleted); //Write the singular 'bitfield64' uint64

        writer.Close();
        stream.Close();
    }

    public static void LoadFromFile()
    {
        eventsCompleted = 0;

        if (!File.Exists(eventsFileLocation)) return; //Don't load anything if the file is empty

        FileStream stream = File.Open(eventsFileLocation, FileMode.Open); //Open the file now that it does exist
        if (stream.Length != 8) //If the file is invalid, close the steam and don't load anything
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