using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Data;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly DataContext _context;

		public class LoginResult {
			public LoginResult (string Username, string UserId, bool IsAdmin, string Token)
			{
				token = Token;
				username = Username;
				userId = UserId;
				isAdmin = IsAdmin;
			}

			public string token;
			public string username;
			public string userId;
			public bool isAdmin;
		}

		public LoginController(DataContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			var loggedIn = _context.Users
				.Where(u => u.UserName == username && u.Password == password).FirstOrDefault();

			if (loggedIn == null)
				throw new UnauthorizedAccessException();

			var token = Guid.NewGuid().ToString();
			loggedIn.Token = token;

			_context.Users.Update(loggedIn);

			var model = new LoginResult(loggedIn.UserName, loggedIn.Id.ToString(), loggedIn.IsAdmin, loggedIn.Token);

			return Ok(model);
		}

		[HttpPost("logout/{userId}")]
		public IActionResult Logout(long userId)
		{
			var loggedIn = _context.Users
				.Where(u => u.Id == userId).FirstOrDefault();

			if (loggedIn == null)
				throw new UnauthorizedAccessException();

			loggedIn.Token = null;

			_context.Users.Update(loggedIn);

			var model = new LoginResult(loggedIn.UserName, loggedIn.Id.ToString(), loggedIn.IsAdmin, loggedIn.Token);

			return Ok(model);
		}
	}
}