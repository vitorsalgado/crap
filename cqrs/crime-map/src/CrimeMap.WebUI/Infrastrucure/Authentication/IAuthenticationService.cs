namespace CrimeMap.WebUI.Infrastructure.Authentication {
	
	public interface IAuthenticationService {

		void SignIn(AppIdentity user, bool isPersistent);

		void SignOut();

		AppIdentity GetAuthenticatedUser();

	}
}
