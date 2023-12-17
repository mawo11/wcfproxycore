using System.ServiceModel;

namespace WcfClientProxy;

public class WcfClient<T> where T : class
{
	private readonly ChannelFactory<T> _channelFactory;
	private readonly T _channel;

	public WcfClient(string endpointAddr)
	{
		var endpoint = new EndpointAddress(endpointAddr);
		BasicHttpBinding baseHttpBinding = new()
		{
			MaxBufferSize = int.MaxValue,
			ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max,
			MaxReceivedMessageSize = int.MaxValue,
			AllowCookies = true
		};

		_channelFactory = new ChannelFactory<T>(baseHttpBinding, endpoint);

		_channel = _channelFactory.CreateChannel();
	}


	public T Channel => _channel;
}
