using System.Web.Mvc;
using CrimeMap.WebUI.Infrastructure.Context;
using CrimeMap.WebUI.ViewModels.Shared;

namespace CrimeMap.WebUI.Controllers {

	public class HomeController : Controller {

		private readonly IWorkContext _workContext;

		public HomeController(IWorkContext workContext) {
			_workContext = workContext;
		}

		public ActionResult Index() {
			return View();
		}

		[ChildActionOnly]
		public ActionResult Nav() {

			var model = new Nav();
			model.IsAuthenticated = _workContext.IsAuthenticated;

			if (_workContext.IsAuthenticated) {
				model.Username = _workContext.GetAppIdentity.Name;
			}

			return PartialView("_Nav", model);
		}
	}
}