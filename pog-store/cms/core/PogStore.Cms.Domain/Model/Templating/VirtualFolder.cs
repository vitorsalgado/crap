using PogStore.Cms.Core.Cqrs.Domain;
using System.Collections.Generic;

namespace PogStore.Cms.Core.Core.Domain.Templating
{
	public class VirtualFolder : AggregateRoot
	{
		private readonly IEnumerable<VirtualFolder> _children;
		private readonly IEnumerable<PageLayout> _pageLayouts;

		public VirtualFolder(string name, string path, VirtualFolder parent, IEnumerable<VirtualFolder> children, IEnumerable<PageLayout> pageLayouts)
		{
			Parent = parent;
			_children = children;
			_pageLayouts = pageLayouts;
		}

		public string Name { get; private set; }

		public string Path { get; private set; }

		public VirtualFolder Parent { get; private set; }

		public IEnumerable<VirtualFolder> Children
		{
			get { return _children; }
		}

		public IEnumerable<PageLayout> Layouts
		{
			get { return _pageLayouts; }
		}
	}
}