namespace FidsCodingAssignment.Common.Helpers;

public static class LogHelper
{
    public static void LogException(Exception exception)
    {
        // TODO: Log exception to proper platform.
        Console.WriteLine(exception);
    }
}