using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrimeMap.DataTransferObjects.Users;
using CrimeMap.Encryption;
using CrimeMap.MongoDb;
using CrimeMap.QueryModel.Users.Models;
using MongoDB.Driver.Linq;

namespace CrimeMap.QueryModel.Users {

	public class UserQueryService : IUserQueryService {

		private readonly IEncryptionService _encryptionService;

		public UserQueryService(IEncryptionService encryptionService) {
			_encryptionService = encryptionService;
		}

		public async Task<UserLoginDto> GetByLogin(string username, string password) {
			if (string.IsNullOrEmpty(username)) {
				throw new ArgumentNullException("username");
			}

			var mongodb = MongoDbConnector.Get();
			var collection = mongodb.GetCollection<UserLoginModel>("userlogin");

			var query = collection.AsQueryable()
				.FirstOrDefault(x => x.Email == username);

			if (query == null) {
				return null;
			}

			var incomingPasswordHashed = _encryptionService.CreatePasswordHash(password, query.Salt, "SHA1");
			var isValid = incomingPasswordHashed.Equals(query.EncryptedPassword);

			if (!isValid) {
				return null;
			}

			return Mapper.Map<UserLoginDto>(query);
		}

		public async Task<UserLoginDto> GetByUsername(string username) {
			if (string.IsNullOrEmpty(username)) {
				throw new ArgumentNullException("username");
			}

			var mongodb = MongoDbConnector.Get();
			var collection = mongodb.GetCollection<UserLoginModel>("userlogin");

			var query = collection.AsQueryable()
				.FirstOrDefault(x => x.Email == username);

			return Mapper.Map<UserLoginDto>(query);
		}
	}

}
