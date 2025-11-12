namespace WinCommon;

/// <summary>
/// Common utilities for Windows applications
/// </summary>
public static class CommonUtilities
{
    /// <summary>
    /// Get application version
    /// </summary>
    public static string GetAppVersion()
    {
        return "1.0.0";
    }

    /// <summary>
    /// Validate a string is not empty
    /// </summary>
    public static bool IsValidString(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
}
