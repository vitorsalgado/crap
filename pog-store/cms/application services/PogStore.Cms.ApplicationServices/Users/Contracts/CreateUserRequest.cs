using PogStore.Cms.Core.Framework;
using System;
using System.Runtime.Serialization;

namespace PogStore.Cms.ApplicationServices.Users.Contracts
{
	[DataContract]
	public class CreateUserRequest : BaseRequest
	{
		[DataMember]
		public Guid UserId { get; private set; }

		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public string Email { get; private set; }

		[DataMember]
		public string Password { get; private set; }

		public CreateUserRequest(Guid userId, string name, string email, string password)
		{
			this.UserId = userId;
			this.Name = name;
			this.Email = email;
			this.Password = password;
		}
	}
}