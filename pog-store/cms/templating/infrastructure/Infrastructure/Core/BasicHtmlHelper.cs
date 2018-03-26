using System.Web.Mvc;

namespace PogStore.Cms.Templating.Core
{
	public static class BasicHtmlHelper
	{
		public static MvcHtmlString HelloWorld(this HtmlHelper htmlHelper)
		{
			return new MvcHtmlString("TESTE EXTENSION");
		}
	}
}