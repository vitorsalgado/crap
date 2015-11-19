using System.Threading.Tasks;
using CrimeMap.DataTransferObjects.Users;

namespace CrimeMap.QueryModel.Users {
	
	public interface IUserQueryService {

		Task<UserLoginDto> GetByLogin(string username, string password);

		Task<UserLoginDto> GetByUsername(string username);

	}

}
