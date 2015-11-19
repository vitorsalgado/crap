using AutoMapper;
using CrimeMap.DataTransferObjects.Users;
using CrimeMap.QueryModel.Users.Models;

namespace CrimeMap.QueryModel.Infrastructure.Mappping {

	public class UserLoginMap : Profile {
	
		protected override void Configure() {
			Mapper.CreateMap<UserLoginModel, UserLoginDto>();
		}

	}

}
