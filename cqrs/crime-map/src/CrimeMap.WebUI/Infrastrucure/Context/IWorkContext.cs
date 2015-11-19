using CrimeMap.WebUI.Infrastructure.Authentication;

namespace CrimeMap.WebUI.Infrastructure.Context {

	public interface IWorkContext {

		AppIdentity GetAppIdentity { get; }

		bool IsAuthenticated { get; }

	}

}
