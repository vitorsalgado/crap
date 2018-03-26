using System.Web.Mvc;

namespace CrimeMap.WebUI.Infrastructure.Controllers {

	public abstract class AbstractController : Controller {

		protected override void OnActionExecuted(ActionExecutedContext filterContext) {
			if (TempData["ModelState"] != null && !ModelState.Equals(TempData["ModelState"]))
				ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);

			base.OnActionExecuted(filterContext);
		}

		protected void PreserveTempData() {
			TempData["ModelState"] = ModelState;
		}

		protected ActionResult RedirectToLocal(string returnUrl) {

			if (Url.IsLocalUrl(returnUrl)) {
				return Redirect(returnUrl);
			}

			return RedirectToAction("Index", "Home");
		}

	}

}