namespace CrimeMap.WebUI.Infrastructure.Authentication {

	public class AppIdentity {

		public string Id { get; private set; }

		public string Name { get; private set; }

		public string Email { get; private set; }

		public AppIdentity(string id, string name, string email) {
			this.Id = id;
			this.Name = name;
			this.Email = email;
		}

	}
}
