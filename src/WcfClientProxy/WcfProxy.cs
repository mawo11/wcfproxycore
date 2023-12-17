using Microsoft.Extensions.Logging;
using System.Reflection;

namespace WcfClientProxy;

public class WcfProxy<T> : DispatchProxy where T : class
{
	private WcfClient<T>? _client;
	private Type? _channelType;
	private ILogger<WcfProxy<T>>? _logger;

	public static T Create(string endpointAddr, ILogger<WcfProxy<T>>? logger)
	{
		object proxy = Create<T, WcfProxy<T>>();
		((WcfProxy<T>)proxy).Initialize(endpointAddr, logger);
		return (T)proxy;
	}

	private void Initialize(string endpointAddr, ILogger<WcfProxy<T>>? logger)
	{
		_logger = logger;
		_client = new WcfClient<T>(endpointAddr);
		_channelType = _client.Channel.GetType();
	}

	protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
	{
		if (targetMethod == null || _channelType == null || _client == null)
		{
			return null;
		}

		try
		{
			_logger?.LogDebug("Invoking method {TypeName}.{MethodName}", targetMethod.DeclaringType.Name, targetMethod.Name);
			return _channelType
					.GetMethod(targetMethod.Name)!
					.Invoke(_client.Channel, args);
		}
		catch (TargetInvocationException exc)
		{
			_logger?.LogError(exc, "Method {TypeName}.{MethodName} threw exception: {Exception}", targetMethod.DeclaringType.Name, targetMethod.Name, exc.InnerException);

			throw exc.InnerException;
		}
	}
}
