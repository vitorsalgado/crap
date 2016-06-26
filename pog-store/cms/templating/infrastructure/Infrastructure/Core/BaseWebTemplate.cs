using RazorEngine.Templating;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PogStore.Cms.Templating.Core
{
	public class BaseWebTemplate<T> : TemplateBase<T>
	{
		private UrlHelper urlHelper;

		public BaseWebTemplate()
		{
			HttpContext.Current = new HttpContext(
				new HttpRequest("a", "http://templating.pogstore.com.br", string.Empty),
				new HttpResponse(new StringWriter()));

			urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
		}

		public UrlHelper Url
		{
			get
			{
				return urlHelper;
			}
		}

		public HtmlHelper<T> Html
		{
			get
			{
				var viewDataContainer = new FakeViewDataContainer(new ViewDataDictionary());
				return new HtmlHelper<T>(new ViewContext(), viewDataContainer);
			}
		}
	}
}