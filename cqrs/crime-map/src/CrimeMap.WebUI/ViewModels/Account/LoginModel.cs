using System.ComponentModel.DataAnnotations;

namespace CrimeMap.WebUI.ViewModels.Account {

	public class LoginModel {

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }

	}

}