using System.ServiceModel;

namespace WcfService.Contracts
{
	[ServiceContract]
	public interface IWorkService
	{
		[OperationContract]
		ResponseItem[] DoWork(long[] ids);
	}
}
