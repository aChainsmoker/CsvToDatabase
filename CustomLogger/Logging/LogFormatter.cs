using System.Text;

namespace CustomLogger.Logging;

public class LogFormatter : ILogFormatter
{
    public string FormatLog(LogLevel level, string? message, Exception? ex= null)
    {
        var messageStringBuilder = new StringBuilder();
        messageStringBuilder.Append($"[{TimeOnly.FromDateTime(DateTime.Now)}]{level}: {message}");
        if (ex != null)
        {
            messageStringBuilder.Append($"An exception of {ex.GetType().Name} type was logged: {ex.Message}");
        }

        return messageStringBuilder.ToString();
    }
}