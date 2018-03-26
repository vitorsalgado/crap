namespace CrimeMap.Encryption {

	public interface IEncryptionService {

		string CreateSaltKey(int size);

		string CreatePasswordHash(string password, string saltKey, string passwordFormat);

		string Encrypt(string value, string key, string iv);

		string Decrypt(string value, string key, string iv);

	}

}
