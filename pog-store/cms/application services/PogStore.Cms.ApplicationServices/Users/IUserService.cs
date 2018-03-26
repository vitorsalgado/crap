using PogStore.Cms.ApplicationServices.Users.Contracts;
using PogStore.Cms.Core.Framework.Messages;

namespace PogStore.Cms.ApplicationServices.Users
{
	public interface IUserService
	{
		Response Create(CreateUserRequest request);
	}
}