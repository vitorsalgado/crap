using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PogStore.Infrascture.Encryption
{
	public class TripleDESEncryptionService : IEncryptionService
	{
		private static TripleDESCryptoServiceProvider _des3;
		private const string DEFAULT_PASSWORD_FORMAT = "SHA1";

		public TripleDESEncryptionService()
		{
			_des3 = new TripleDESCryptoServiceProvider();
			_des3.Mode = CipherMode.CBC;
		}

		public string CreateSaltKey(int size)
		{
			using (var rng = new RNGCryptoServiceProvider())
			{
				var buff = new byte[size];
				rng.GetBytes(buff);

				return Convert.ToBase64String(buff);
			}
		}

		public string CreatePasswordHash(string password, string salt, string passwordFormat)
		{
			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException("password");
			}

			if (string.IsNullOrEmpty(salt))
			{
				throw new ArgumentNullException("salt");
			}

			if (string.IsNullOrEmpty(password))
			{
				passwordFormat = DEFAULT_PASSWORD_FORMAT;
			}

			string saltAndPassword = string.Concat(password, salt);

			using (var algorithm = HashAlgorithm.Create(passwordFormat))
			{
				var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
				return BitConverter.ToString(hashByteArray).Replace("-", "");
			}
		}

		public string Encrypt(string value, string key, string iv)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException("No values to encrypt.");
			}

			var bkey = new ASCIIEncoding().GetBytes(key.Substring(0, 16));
			var biv = new ASCIIEncoding().GetBytes(iv);

			using (var ms = new MemoryStream())
			{
				using (var cs = new CryptoStream(ms, _des3.CreateEncryptor(bkey, biv), CryptoStreamMode.Write))
				{
					byte[] toEncrypt = new UnicodeEncoding().GetBytes(value);
					cs.Write(toEncrypt, 0, toEncrypt.Length);
					cs.FlushFinalBlock();
				}

				byte[] encryptedBinary = ms.ToArray();

				return Convert.ToBase64String(encryptedBinary);
			}
		}

		public string Decrypt(string value, string key, string iv)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException("value");
			}

			var bkey = new ASCIIEncoding().GetBytes(key.Substring(0, 16));
			var biv = new ASCIIEncoding().GetBytes(iv);

			byte[] buffer = Convert.FromBase64String(value);

			using (var ms = new MemoryStream(buffer))
			{
				using (var cs = new CryptoStream(ms, _des3.CreateDecryptor(bkey, biv), CryptoStreamMode.Read))
				{
					var sr = new StreamReader(cs, new UnicodeEncoding());
					return sr.ReadLine();
				}
			}
		}
	}
}