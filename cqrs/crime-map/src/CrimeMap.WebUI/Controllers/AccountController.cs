using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using CrimaMap.ApiClient.Users;
using CrimeMap.ApplicationServices.Users.Messages;
using CrimeMap.Commands.Users;
using CrimeMap.Common;
using CrimeMap.WebUI.Infrastructure.Authentication;
using CrimeMap.WebUI.Infrastructure.Configuration;
using CrimeMap.WebUI.Infrastructure.Controllers;
using CrimeMap.WebUI.ViewModels.Account;
using Facebook;

namespace CrimeMap.WebUI.Controllers {

	public class AccountController : AbstractController {

		private const string FACEBOOK_PROVIDER = "facebook";

		private readonly IConfigurationService _configurationService;
		private readonly IAuthenticationService _authenticationService;
		private readonly IUserProxy _userProxy;

		public AccountController(
			IConfigurationService configurationService,
			IUserProxy userProxy,
			IAuthenticationService authenticationService) {
			_configurationService = configurationService;
			_userProxy = userProxy;
			_authenticationService = authenticationService;
		}

		[AllowAnonymous]
		public ActionResult SignIn(string returnUrl) {

			if (string.IsNullOrEmpty(returnUrl)) {
				returnUrl = "/";
			}

			var model = new LoginModel();
			model.ReturnUrl = returnUrl;

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SignIn(LoginModel model, string returnUrl) {

			if (ModelState.IsValid) {
				var request = new LoginRequest();
				request.Password = model.Password;
				request.Username = model.Email;

				var response = await _userProxy.GetLoginInformation(request);

				if (response.Ack == AckType.SUCCESS) {
					var data = response.Data;
					var identity = new AppIdentity(data.UserId.ToString(), data.Name, data.Email);

					_authenticationService.SignIn(identity, true);

					return RedirectToLocal(returnUrl);
				}
			}

			ModelState.AddModelError("", "Username or password incorret.");

			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult FacebookSignIn(string returnUrl) {
			if (string.IsNullOrEmpty(returnUrl))
				returnUrl = "/";

			var callbackUrl = BuildFacebookSignInCallbackUrl();
			var loginUrl = FacebookOAuthUri(callbackUrl);

			return Redirect(loginUrl.AbsoluteUri);
		}

		public async Task<ActionResult> FacebookSignInCallback(string code, string returnUrl) {
			var facebookClient = new FacebookClient();
			var redirectUri = BuildFacebookSignInCallbackUrl();

			dynamic result = facebookClient.Post("oauth/access_token", new {
				client_id = _configurationService.FacebookAppId,
				client_secret = _configurationService.FacebookAppSecret,
				redirect_uri = redirectUri,
				code = code
			});

			var access_token = result.access_token;
			facebookClient.AccessToken = access_token;

			dynamic me = facebookClient.Get("me");

			string email = me.email;
			string name = me.name;
			string id = me.id;

			var userId = Guid.NewGuid();
			var command = new RegisterExternalUserCommand(userId, name, email, FACEBOOK_PROVIDER, id);

			var response = await _userProxy.RegisterExternalUserIfNeeded(command);

			if (response.Ack == AckType.SUCCESS) {
				var appIdentity = new AppIdentity(userId.ToString(), command.Name, command.Email);
				_authenticationService.SignIn(appIdentity, true);

				return RedirectToLocal(returnUrl);
			} else {
				ModelState.AddModelError("", response.Message);
				PreserveTempData();

				return RedirectToAction("SignIn", new { returnUrl = returnUrl });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SignOut() {
			_authenticationService.SignOut();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Facebook(string returnUrl) {

			if (string.IsNullOrEmpty(returnUrl))
				returnUrl = "/";

			var callbackUrl = BuildFacebookRegisterCallbackUrl();
			var redirectUri = FacebookOAuthUri(callbackUrl);

			return Redirect(redirectUri.AbsoluteUri);
		}

		public async Task<ActionResult> FacebookRegisterCallback(string code, string returnUrl) {

			var facebookClient = new FacebookClient();
			var redirectUri = BuildFacebookRegisterCallbackUrl();

			dynamic result = facebookClient.Post("oauth/access_token", new {
				client_id = _configurationService.FacebookAppId,
				client_secret = _configurationService.FacebookAppSecret,
				redirect_uri = redirectUri,
				code = code
			});

			var access_token = result.access_token;
			facebookClient.AccessToken = access_token;

			dynamic me = facebookClient.Get("me");
			string email = me.email;
			string name = me.name;
			string id = me.id;

			var userId = Guid.NewGuid();
			var command = new RegisterExternalUserCommand(userId, name, email, FACEBOOK_PROVIDER, id);

			var response = await _userProxy.RegisterExternalUserIfNeeded(command);

			if (response.Ack == AckType.SUCCESS) {
				var appIdentity = new AppIdentity(userId.ToString(), command.Name, command.Email);
				_authenticationService.SignIn(appIdentity, true);

				return RedirectToLocal(returnUrl);
			} else {
				ModelState.AddModelError("", response.Message);
				PreserveTempData();

				return RedirectToAction("Register", new { returnUrl = returnUrl });
			}
		}

		[AllowAnonymous]
		public ActionResult Register(string returnUrl) {
			if (string.IsNullOrEmpty(returnUrl))
				returnUrl = "/";

			var model = new RegisterModel();
			model.ReturnUrl = returnUrl;

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterModel model, string returnUrl) {

			if (!ModelState.IsValid) {
				return View(model);
			}

			var userId = Guid.NewGuid();
			var command = new RegisterUserCommand(userId, model.Name, model.Email, model.Password);

			var response = await _userProxy.Register(command);

			if (response.Ack == AckType.SUCCESS) {
				var appIdentity = new AppIdentity(userId.ToString(), command.Name, command.Email);
				_authenticationService.SignIn(appIdentity, true);

				return RedirectToLocal(returnUrl);
			}

			return View(model);
		}

		private string BuildFacebookRegisterCallbackUrl() {

			var uriBuilder = new UriBuilder(Request.Url);
			uriBuilder.Path = Url.Action("FacebookRegisterCallback", "Account");

			return uriBuilder.Uri.AbsoluteUri;
		}

		private string BuildFacebookSignInCallbackUrl() {

			var uriBuilder = new UriBuilder(Request.Url);
			uriBuilder.Path = Url.Action("FacebookSignInCallback", "Account");

			return uriBuilder.Uri.AbsoluteUri;
		}

		private Uri FacebookOAuthUri(string callbackUrl) {
			var facebookClient = new FacebookClient();
			var redirectUri = callbackUrl;

			var loginUrl = facebookClient.GetLoginUrl(new {
				client_id = _configurationService.FacebookAppId,
				client_secret = _configurationService.FacebookAppSecret,
				redirect_uri = redirectUri,
				response_type = "code",
				scope = "email"
			});

			return loginUrl;
		}

	}

}
