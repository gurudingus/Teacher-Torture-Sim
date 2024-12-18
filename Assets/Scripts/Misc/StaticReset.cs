using System.Collections.Generic;

/// <summary>
/// Anything that needs to have a static variable reset can implement this interface
/// </summary>
public interface IResetStatic
{
    public void OnResetStatic();
}

public static class StaticReset
{
    private static List<IResetStatic> subscribers = new(); //A list of all objects implementing IStaticReset that will have a function that resets static variables to their default whenever the reset function in this script is called


    //Resets all static variables that have been changed.
    public static void ResetStatics()
    {
        foreach (IResetStatic subscriber in subscribers) subscriber.OnResetStatic();
        subscribers.Clear();
    }

    public static void Subscribe(IResetStatic subscriber) => subscribers.Add(subscriber);
}
