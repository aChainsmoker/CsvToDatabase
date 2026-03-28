namespace CustomLogger.Logging;

public class Logger : ILogger
{
    private readonly List<ILoggerDestination> _loggers;
    private readonly LogFormatter _formatter;

    public Logger()
    {
        _loggers = [new ConsoleLogger()];
        _formatter = new LogFormatter();
    }
    
    public Logger(List<ILoggerDestination> loggers)
    {
        _loggers = loggers;
        _formatter = new LogFormatter();
    }

    public void AddLoggerItem(ILoggerDestination destination)
    {
        _loggers.Add(destination);
    }

    public void RemoveLoggerItem(ILoggerDestination destination)
    {
        _loggers.Remove(destination);
    }
    
    public void Error(string message)
    {
        Log(LogLevel.Error, message);
    }

    public void Error(Exception ex)
    {
        Log(LogLevel.Error, null, ex);
    }

    public void Warning(string message)
    {
        Log(LogLevel.Warning, message);
    }

    public void Info(string message)
    {
        Log(LogLevel.Info, message);
    }

    private void Log(LogLevel level, string? message, Exception? ex = null)
    {
        foreach (var logger in _loggers)
        {
            if (logger.LoggingLevel != null && level > logger.LoggingLevel)
            {
                continue;
            }
            logger.Log(_formatter.FormatLog(level, message, ex));
        }
    }
}