using System;

namespace SmartNotificationManager;

/// <summary>
/// Entry point for the Smart Notification Manager application
/// </summary>
class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        Console.WriteLine("Smart Notification Manager - .NET 9 Application");
        Console.WriteLine("This application is configured for WinUI3 when built on Windows.");
        Console.WriteLine("Currently running in console mode for CI/CD compatibility.");
        
        // When built on Windows with UseWinUI=true, the actual WinUI3 app will launch
        // For now, this provides a basic entry point for testing
    }
}
