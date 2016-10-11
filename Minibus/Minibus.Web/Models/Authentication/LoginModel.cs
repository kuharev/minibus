using System.ComponentModel.DataAnnotations;

namespace Minibus.Web.Models.Authentication
{
	public class LoginModel
	{
		[Required]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
	}
}