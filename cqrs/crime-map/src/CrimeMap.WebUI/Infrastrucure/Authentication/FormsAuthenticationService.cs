using System;
using System.Diagnostics.Contracts;
using System.Web;
using System.Web.Security;

namespace CrimeMap.WebUI.Infrastructure.Authentication {

	public class FormsAuthenticationService : IAuthenticationService {

		private readonly HttpContextBase _httpContext;
		private AppIdentity _cachedUser;

		public FormsAuthenticationService(HttpContextBase httpContext) {
			_httpContext = httpContext;
		}

		public void SignIn(AppIdentity user, bool isPersistent) {
			var now = DateTime.UtcNow.ToLocalTime();
			string userData = string.Format("{0};{1};{2}", user.Id, user.Name, user.Email);

			var ticket = new FormsAuthenticationTicket(
				1,
				user.Email,
				now,
				now.Add(FormsAuthentication.Timeout),
				isPersistent,
				userData,
				FormsAuthentication.FormsCookiePath);

			var encryptedTicket = FormsAuthentication.Encrypt(ticket);
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

			cookie.HttpOnly = true;

			if (ticket.IsPersistent) {
				cookie.Expires = ticket.Expiration;
			}

			cookie.Secure = FormsAuthentication.RequireSSL;
			cookie.Path = FormsAuthentication.FormsCookiePath;

			if (FormsAuthentication.CookieDomain != null) {
				cookie.Domain = FormsAuthentication.CookieDomain;
			}

			_httpContext.Response.Cookies.Add(cookie);
			_cachedUser = user;
		}

		public void SignOut() {
			FormsAuthentication.SignOut();
		}

		public AppIdentity GetAuthenticatedUser() {
			if (_cachedUser != null)
				return _cachedUser;

			if (!(_httpContext.User.Identity is FormsIdentity))
				return null;

			var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
			string[] userdata = formsIdentity.Ticket.UserData.Split(';');

			string email = formsIdentity.Ticket.Name;
			string id = userdata[0];
			string name = userdata[1];

			var user = new AppIdentity(id, name, email);

			_cachedUser = user;

			return _cachedUser;
		}

	}
}
