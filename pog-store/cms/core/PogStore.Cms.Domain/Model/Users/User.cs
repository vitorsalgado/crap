using PogStore.Cms.Core.Cqrs.Domain;
using System;

namespace CrimeMap.Domain.Model.Users
{
	public class User : AggregateRoot
	{
		public Guid Identifier { get; private set; }

		public string Name { get; private set; }

		public string Password { get; private set; }

		public string PasswordSalt { get; private set; }

		public string Email { get; private set; }

		public User()
		{
			RegisterEvents();
		}

		public User(Guid id, string name, string email, string password, string passwordSalt, string passwordFormat)
		{
			RegisterEvents();

			//var @event = new UserCreatedEvent(id, name, email, password, passwordSalt);
			//RaiseEvent(@event);
		}

		private void RegisterEvents()
		{
			//RegisterEvent<UserCreatedEvent>(OnCreated);
		}

		//void OnCreated(UserCreatedEvent @event) {
		//	Id = @event.Id;
		//	Name = @event.Name;
		//	Email = @event.Email;
		//	Password = @event.Password;
		//	PasswordSalt = @event.PasswordSalt;

		//	Accounts = new List<Account>();
		//}
	}
}