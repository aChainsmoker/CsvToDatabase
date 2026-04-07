namespace CustomLogger.Logging;

public interface ILoggerDestination
{
    LogLevel? LoggingLevel { get; init; }
    void Log(string message);
}