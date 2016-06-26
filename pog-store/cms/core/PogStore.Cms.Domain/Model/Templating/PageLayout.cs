using PogStore.Cms.Core.Cqrs.Domain;

namespace PogStore.Cms.Core.Core.Domain.Templating
{
	public class PageLayout : AggregateRoot
	{
		public string Name { get; private set; }

		public bool Default { get; private set; }

		public VirtualFolder Folder { get; private set; }

		public Template Template { get; private set; }

		public PageLayout(string name, bool isDefault, VirtualFolder folder, Template template)
		{
			this.Template = template;
		}
	}
}