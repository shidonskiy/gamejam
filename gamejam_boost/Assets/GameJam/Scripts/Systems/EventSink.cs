using System;

//Rough but fast way to implement even communication between systems
public static class EventSink
{
    public static event Action GameStart;

    public static void OnGameStart()
    {
        GameStart?.Invoke();
    }
}
