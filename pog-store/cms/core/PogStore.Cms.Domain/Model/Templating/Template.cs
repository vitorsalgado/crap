using PogStore.Cms.Core.Cqrs.Domain;

namespace PogStore.Cms.Core.Core.Domain.Templating
{
	public class Template : AggregateRoot
	{
		public string Name { get; private set; }

		public string Group { get; private set; }

		public string Html { get; private set; }

		public Template(string name, string group, string html)
		{
			Name = name;
			Group = group;
			Html = html;
		}
	}
}