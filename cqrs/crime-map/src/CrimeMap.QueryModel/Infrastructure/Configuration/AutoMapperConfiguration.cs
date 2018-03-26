using AutoMapper;
using CrimeMap.QueryModel.Infrastructure.Mappping;

namespace CrimeMap.QueryModel.Infrastructure.Configuration {

	public static class AutoMapperConfiguration {

		public static void SetUp() {
			Mapper.Initialize(x => {
				x.AddProfile<UserLoginMap>();
			});
		}
	}
}
