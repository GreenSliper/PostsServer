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
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AuthController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		IUserService userService;
		SignInManager<AppUser> signInManager;
		UserManager<AppUser> userManager;
		RoleManager<IdentityRole> roleManager;
		IConfiguration configuration;
		public AuthController(ILogger<HomeController> logger, IUserService userService,
			SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IConfiguration configuration)
		{
			_logger = logger;
			this.userService = userService;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.configuration = configuration;
			this.roleManager = roleManager;
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromForm] RegisterViewModel model)
		{
			AppUser user = await userService.Login(model.UserName, model.Password);
			if (user != null)
			{
				var userRoles = await userManager.GetRolesAsync(user);

				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id),
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				};

				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}

				var token = GetToken(authClaims);

				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					expiration = token.ValidTo
				});
			}
			return Unauthorized();
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
		{
			var userExists = await userManager.FindByNameAsync(model.UserName);
			if (userExists != null)
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseModel { Status = "Error", Message = "User already exists!" });

			AppUser user = new AppUser()
			{
				Email = model.UserName + "@gmail.com",
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = model.UserName
			};
			var result = await userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseModel
					{
						Status = "Error",
						Message = "User creation failed! Please check user details and try again."
					});

			return Ok(new ResponseModel { Status = "Success", Message = "User created successfully!" });
		}

		[HttpPost]
		[Route("register-admin")]
		public async Task<IActionResult> RegisterAdmin([FromBody] RegisterViewModel model)
		{
			var userExists = await userManager.FindByNameAsync(model.UserName);
			if (userExists != null)
				return StatusCode(StatusCodes.Status500InternalServerError, 
					new ResponseModel { Status = "Error", Message = "User already exists!" });

			AppUser user = new AppUser()
			{
				Email = model.UserName + "@gmail.com",
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = model.UserName
			};
			var result = await userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseModel { Status = "Error", 
						Message = "User creation failed! Please check user details and try again." });

			if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
				await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
			if (!await roleManager.RoleExistsAsync(UserRoles.User))
				await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

			if (await roleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await userManager.AddToRoleAsync(user, UserRoles.Admin);
			}
			if (await roleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await userManager.AddToRoleAsync(user, UserRoles.User);
			}
			return Ok(new ResponseModel { Status = "Success", Message = "User created successfully!" });
		}

		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(
				issuer: configuration["JWT:ValidIssuer"],
				audience: configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddHours(3),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return token;
		}
	}
}
