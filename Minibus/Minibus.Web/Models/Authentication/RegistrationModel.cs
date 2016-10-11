using System.ComponentModel.DataAnnotations;
using Minibus.Data.Model;

namespace Minibus.Web.Models.Authentication
{
	public class RegistrationModel
	{
		[Required]
		public string Login { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		public User CreateUser()
		{
			return new User
			{
				Login = Login,
				Email = Email,
				Password = Password
			};
		}
	}
}