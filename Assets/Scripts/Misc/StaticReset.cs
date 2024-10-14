using System.Collections.Generic;

/// <summary>
/// Anything that needs to have a static variable reset can implement this interface
/// </summary>
public interface IResetStatic
{
    public void OnStaticReset();
}

public static class StaticReset
{
    private static List<IResetStatic> subscribers = new(); //A list of all objects implementing IStaticReset that will have a function that resets static variables to their default whenever the reset function in this script is called

    public static void ResetStatics()
    {
        foreach (IResetStatic subscriber in subscribers) subscriber.OnStaticReset();
        subscribers.Clear();
    }

    public static void Subscribe(IResetStatic subscriber) => subscribers.Add(subscriber);
}
