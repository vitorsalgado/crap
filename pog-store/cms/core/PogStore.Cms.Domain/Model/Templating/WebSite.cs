using PogStore.Cms.Core.Cqrs.Domain;
using System.Collections.Generic;

namespace PogStore.Cms.Core.Core.Domain.Templating
{
	public class WebSite : AggregateRoot
	{
		private IEnumerable<VirtualFolder> _virtualFolders;

		public string Name { get; set; }

		public string Description { get; set; }

		public IEnumerable<VirtualFolder> VirtualFolders
		{
			get
			{
				return _virtualFolders ?? (_virtualFolders = new List<VirtualFolder>());
			}
		}
	}
}