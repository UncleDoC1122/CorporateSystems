using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Platform.Models
{
	public class User
	{
		[Key]
		public long Id { get; set; }

		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string MiddleName { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public bool IsAdmin { get; set; }

		public string Token { get; set; }
	}
}
