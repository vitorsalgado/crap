using CrimeMap.Domain.Model.Users;
using CrimeMap.EventHandlers.Infrastructure;
using CrimeMap.MongoDb;
using CrimeMap.QueryModel.Users.Models;

namespace CrimeMap.EventHandlers.Users {

	public class UserCreatedHandler : EventHandler<UserCreatedEvent> {

		public override void Handle(UserCreatedEvent @event) {
			var mongodb = MongoDbConnector.Get();
			var collection = mongodb.GetCollection<UserLoginModel>("userlogin");

			var model = new UserLoginModel();
			model.Email = @event.Email;
			model.EncryptedPassword = @event.Password;
			model.UserId = @event.Id;
			model.Name = @event.Name;
			model.Salt = @event.PasswordSalt;

			collection.Insert(model);
		}
	}
}
