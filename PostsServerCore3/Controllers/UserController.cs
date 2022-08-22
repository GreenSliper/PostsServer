using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostsServerCore3.Models;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PostsServerCore3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		IUserService userService;
		UserManager<AppUser> userManager;
		IMapper mapper;
		public UserController(IUserService userService, UserManager<AppUser> userManager, IMapper mapper)
		{
			this.userService = userService;
			this.userManager = userManager;
			this.mapper = mapper;
		}

		[Authorize]
		[HttpGet]
		[Route("current")]
		public async Task<IActionResult> GetCurrentUser()
		{
			var user = await userManager.GetUserAsync(User);
			return Content(JsonSerializer.Serialize(mapper.Map<AppUserViewModel>(user)));
		}

		[Authorize]
		[HttpGet]
		[Route("subscriptions")]
		public async Task<IActionResult> GetCurrentUserSubscriptions()
		{
			var user = await userManager.GetUserAsync(User);
			return Content(JsonSerializer.Serialize(mapper.Map<List<AppUserViewModel>>(user.Subscriptions.Select(x=>x.SubTarget))));
		}

		[Authorize]
		[HttpGet]
		[Route("subscribe")]
		public async Task<IActionResult> Subscribe([FromForm]string targetUserId)
		{
			var user = await userManager.GetUserAsync(User);
			await userService.Subscribe(user.Id, targetUserId);
			return Ok(new ResponseModel("Success!"));
		}

		[Authorize]
		[HttpGet]
		[Route("unsubscribe")]
		public async Task<IActionResult> Unsubscribe([FromForm] string targetUserId)
		{
			var user = await userManager.GetUserAsync(User);
			await userService.Unsubscribe(user.Id, targetUserId);
			return Ok(new ResponseModel("Success!"));
		}
	}
}
