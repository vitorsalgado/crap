using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeMap.WebUI.ViewModels.Account {

	public class RegisterModel {

		[Required]
		public string Name { get; set; }

		[Required]
		[DisplayName("Email")]
		public string Email { get; set; }

		public string Password { get; set; }

		[DisplayName("Password Confirmation")]
		public string PasswordConfirmation { get; set; }

		public string ReturnUrl { get; set; }

	}
}
