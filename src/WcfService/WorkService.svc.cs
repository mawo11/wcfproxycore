using System.Linq;
using WcfService.Contracts;

namespace WcfService
{
	public class WorkService : IWorkService
	{
		public ResponseItem[] DoWork(long[] ids)
		{
			return ids
				.Select(id => new ResponseItem
				{
					Id = id,
					Name = (id * 2).ToString()
				})
				.ToArray();
		}
	}
}
