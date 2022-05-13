using System;
using GameJam.Scripts.Levels;

//Rough but fast way to implement even communication between systems
public static class EventSink
{
    public static event Action GameStart;
    public static event Action LoadLevelStart;
    public static event Action<BaseLevel> LoadLevelFinish;

    public static void OnGameStart()
    {
        GameStart?.Invoke();
    }

    public static void OnLoadLevelStart()
    {
        LoadLevelStart?.Invoke();
    }

    public static void OnLoadLevelFinish(BaseLevel level)
    {
        LoadLevelFinish?.Invoke(level);
    }
}
