using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WcfClientProxy;

public static class SwaggerGenServiceCollectionExtensions
{
	public static IServiceCollection AddWcfProxyClient<T>(this IServiceCollection services, string endpointAddr) where T : class
	{
		services.AddSingleton<T>(services =>
		{
			var proxy = WcfProxy<T>.Create(endpointAddr, services.GetRequiredService<ILogger<WcfProxy<T>>>());
			return proxy;
		});

		return services;
	}
}
