using System;
using CrimeMap.Core.Events;

namespace CrimeMap.Domain.Model.Users {

	public class UserCreatedEvent : Event {

		public Guid Id { get; private set; }

		public string Name { get; private set; }

		public string Email { get; private set; }

		public string Password { get; private set; }

		public string PasswordSalt { get; private set; }

		public UserCreatedEvent(Guid id, string name, string email, string password, string passwordSalt) {
			Id = id;
			Name = name;
			Email = email;
			Password = password;
			PasswordSalt = passwordSalt;
		}

	}

}
