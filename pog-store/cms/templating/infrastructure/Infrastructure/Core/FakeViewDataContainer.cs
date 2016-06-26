using System.Web.Mvc;

namespace PogStore.Cms.Templating.Core
{
	public class FakeViewDataContainer : IViewDataContainer
	{
		public FakeViewDataContainer(ViewDataDictionary viewData)
		{
			ViewData = viewData;
		}

		public ViewDataDictionary ViewData { get; set; }
	}
}