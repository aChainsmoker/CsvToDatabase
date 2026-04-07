using System.Text;

namespace CustomLogger.Logging;

public class FileLogger : ILoggerDestination
{
    private readonly string _filePath;
    public LogLevel? LoggingLevel { get; init; }
    
    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }
    
    public FileLogger(string filePath, LogLevel level) : this(filePath)
    {
        LoggingLevel = level;
    }

    public void Log(string message)
    {
        using var fileStream = new FileStream(_filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        var buffer = Encoding.Default.GetBytes(message);
        fileStream.Write(buffer, 0, buffer.Length);
        fileStream.WriteByte((byte)'\n');
    }
}