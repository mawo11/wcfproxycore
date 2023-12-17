using System.Runtime.Serialization;

namespace WcfService.Contracts
{
	[DataContract]
	public class ResponseItem
	{
		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public long Id { get; set; }
	}
}
