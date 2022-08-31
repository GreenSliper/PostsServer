using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using Repostiory;
using Services;
using System;
using Xunit;

namespace AuthTests
{
	public class LoginTest
	{
		[Theory]
		[InlineData("TestLogin", "Password")]
		[InlineData("TestLogin1", "Password1")]
		public async void GoodLoginTest(string username, string pass)
		{
			var repos = new Mock<IRepository<AppUser, string>>();
			var userManager = UserManagerMoq.Create((username, pass));
			var userService = new UserService(repos.Object, null, null, userManager.Object);
			var user = await userService.Login(username, pass);
		}

		[Theory]
		[InlineData("TestLogin", "Password")]
		[InlineData("TestLogin1", "Password1")]
		public async void BadLoginTest(string username, string pass)
		{
			var repos = new Mock<IRepository<AppUser, string>>();
			var userManager = UserManagerMoq.Create((username, pass+"123"));
			var userService = new UserService(repos.Object, null, null, userManager.Object);
			await Assert.ThrowsAsync<BadCredentialsException>(async () => await userService.Login(username, pass));
		}
	}
}
