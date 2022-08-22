using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using PostsServerCore3.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Azure;
using Microsoft.AspNetCore.Http;

namespace PostsServerCore3.Controllers
{
	//[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		IUserService userService;
		SignInManager<AppUser> signInManager;
		public HomeController(ILogger<HomeController> logger, IUserService userService,
			SignInManager<AppUser> signInManager)
		{
			_logger = logger;
			this.userService = userService;
			this.signInManager = signInManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		[Authorize]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
		{
			try
			{
				var user = new Domain.Models.AppUser() { UserName = model.UserName, Email = model.UserName+"@gmail.com" };
				await userService.RegisterUser(user, model.Password);
				await signInManager.SignInAsync(user, false);
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
			}
			return View();
		}
	}
}
