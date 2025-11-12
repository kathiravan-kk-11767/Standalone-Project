namespace WinLogger;

/// <summary>
/// Logger for Windows applications
/// </summary>
public class Logger
{
    /// <summary>
    /// Log an information message
    /// </summary>
    public static void LogInfo(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
    }

    /// <summary>
    /// Log an error message
    /// </summary>
    public static void LogError(string message, Exception? exception = null)
    {
        Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        if (exception != null)
        {
            Console.WriteLine($"Exception: {exception.Message}");
        }
    }
}
