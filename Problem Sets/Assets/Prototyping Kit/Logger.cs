//THIS SHOULD BE ADDED TO THE TOP OF THE CLASS USING THE LOGGER
//#if UNITY_EDITOR
//#define ENABLE_LOGS
//#endif
using System.Diagnostics;

public static class Logger
{
    [Conditional("ENABLE_LOGS")]
    public static void Log(string logMsg)
    {
        UnityEngine.Debug.Log(logMsg);
    }
    
    [Conditional("ENABLE_LOGS")]
    public static void Warning(string logMsg)
    {
        UnityEngine.Debug.LogWarning(logMsg);
    }
    
    [Conditional("ENABLE_LOGS")]
    public static void Error(string logMsg)
    {
        UnityEngine.Debug.LogError(logMsg);
    }
}