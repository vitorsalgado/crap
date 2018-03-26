namespace PogStore.Cms.Core.Templating.Parser
{
	public class ParseRequest
	{
		public string Name { get; private set; }

		public string Content { get; private set; }

		public object Model { get; private set; }

		public ParseRequest(string name, string content, object model)
		{
			this.Name = name;
			this.Content = content;
			this.Model = model;
		}
	}
}