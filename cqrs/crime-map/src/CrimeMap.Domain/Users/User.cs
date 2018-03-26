using System;
using System.Linq;
using System.Collections.Generic;
using CrimeMap.Core.Domain;

namespace CrimeMap.Domain.Model.Users {

	public class User : AggregateRoot {

		private ICollection<Account> _externalAccounts;

		public string Name { get; private set; }

		public string Password { get; private set; }

		public string PasswordSalt { get; private set; }

		public string Email { get; private set; }

		public ICollection<Account> Accounts {
			get { return _externalAccounts ?? (_externalAccounts = new List<Account>()); }
			private set { _externalAccounts = value; }
		}

		public User() {
			RegisterEvents();
		}

		public User(Guid id, string name, string email, string password, string passwordSalt, string passwordFormat) {
			RegisterEvents();

			var @event = new UserCreatedEvent(id, name, email, password, passwordSalt);
			RaiseEvent(@event);
		}

		public User(Guid id, string name, string email, string provider, string providerKey) {
			RegisterEvents();

			var @event = new ExternalUserCreatedEvent(id, name, email, provider, providerKey);
			RaiseEvent(@event);
		}

		public void AddExternalLogin(string provider, string providerKey) {
			if (string.IsNullOrEmpty(provider))
				throw new ArgumentNullException("provider");

			if (string.IsNullOrEmpty(providerKey))
				throw new ArgumentNullException("providerKey");

			if (Accounts != null && Accounts.Any() && Accounts.Any(x => x.Provider.Equals(provider) && x.ProviderKey.Equals(providerKey))) {
				throw new DomainException("User already contains a login for provider {0}.", provider);
			}

			var @event = new ExternalAccountAddedEvent(provider, providerKey);
			RaiseEvent(@event);
		}

		private void RegisterEvents() {
			RegisterEvent<UserCreatedEvent>(OnCreated);
			RegisterEvent<ExternalUserCreatedEvent>(OnExternalCreated);
			RegisterEvent<ExternalAccountAddedEvent>(OnExternalAccountAdded);
		}

		void OnCreated(UserCreatedEvent @event) {
			Id = @event.Id;
			Name = @event.Name;
			Email = @event.Email;
			Password = @event.Password;
			PasswordSalt = @event.PasswordSalt;

			Accounts = new List<Account>();
		}

		void OnExternalCreated(ExternalUserCreatedEvent @event) {
			Id = @event.Id;
			Name = @event.Name;
			Email = @event.Email;

			var account = new Account(@event.Provider, @event.ProviderKey);
			Accounts.Add(account);
		}

		void OnExternalAccountAdded(ExternalAccountAddedEvent @event) {
			var account = new Account(@event.Provider, @event.ProviderKey);
			Accounts.Add(account);
		}

	}

}
