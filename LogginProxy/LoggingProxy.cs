using System.Dynamic;
using CustomLogger.Logging;
using ImpromptuInterface;

namespace LogginProxy;

public class LoggingProxy<T> : DynamicObject
{
    private readonly T _target;
    private readonly ILogger _logger;

    private LoggingProxy(T target, ILogger logger)
    {
        _target = target;
        _logger = logger;
    }

    public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
    {
        var methodName = binder.Name;

        try
        {
            var method = _target?.GetType().GetMethod(methodName);
            if (method != null)
            {
                result = method.Invoke(_target, args);
                _logger.Info($"Method {methodName} executed successfully with parameters: {String.Join(", ", args?.Select(x=>x.ToString())) }");
                
                return true;
            }
        }
        catch (Exception e)
        {
            _logger.Error($"Method {methodName} threw an exception: {e.Message}");
            throw;
        }
        result = null;
        
        return false;
    }


    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        var propertyName = binder.Name;
        try
        {
            var propertyInfo = _target?.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                result = propertyInfo.GetValue(_target);
                _logger.Info($"Property {propertyName} extracted successfully");
                
                return true;
            }
        }
        catch (Exception e)
        {
            _logger.Error($"Getting property {propertyName} threw an exception: {e.Message}");
            throw;
        }
        result = null;
        
        return false;
    }

    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        var propertyName = binder.Name;
        
        try
        {
            var propertyInfo = _target?.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(_target, value);
                _logger.Info($"Property {propertyName} was successfully set to {value}");
                
                return true;
            }
        }
        catch (Exception e)
        {
            _logger.Error($"Setting property {propertyName} threw an exception: {e.Message}");
            throw;
        }
        
        return false;
    }

    public static T CreateInstance(T obj, ILogger logger)
    {
        var proxy = new LoggingProxy<T>(obj, logger);
        
        return proxy.ActLike();
    }
}