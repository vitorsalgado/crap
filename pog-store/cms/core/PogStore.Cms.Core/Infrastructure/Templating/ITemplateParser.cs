namespace PogStore.Cms.Core.Templating.Parser
{
	public interface ITemplateParser
	{
		string Parse(ParseRequest request);
	}
}