using System.Web;
using CrimeMap.WebUI.Infrastructure.Authentication;

namespace CrimeMap.WebUI.Infrastructure.Context {

	public class WorkContext : IWorkContext {

		private readonly IAuthenticationService _authenticationService;
		private readonly HttpContextBase _httpContext;

		public WorkContext(
			IAuthenticationService authenticationService,
			HttpContextBase httpContext) {

			_authenticationService = authenticationService;
			_httpContext = httpContext;
		}

		public AppIdentity GetAppIdentity {
			get {
				return _authenticationService.GetAuthenticatedUser();
			}
		}

		public bool IsAuthenticated {
			get {
				return _httpContext.User.Identity.IsAuthenticated && this.GetAppIdentity != null;
			}
		}
	}
}
