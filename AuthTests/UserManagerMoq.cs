using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthTests
{
	internal class UserManagerMoq
	{
		public static Mock<UserManager<AppUser>> Create(params (string username, string password)[] users)
		{
			var userManager = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
			userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
				.ReturnsAsync(
					(string x) => {
						var user = users.FirstOrDefault(y => y.username == x);
						if (user != default)
							return new AppUser()
							{
								UserName = user.username,
								PasswordHash = user.password + "Hash"
							};
						return new AppUser();
					}
				);
			userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
				.ReturnsAsync((AppUser u, string p) =>
				{
					return (u.PasswordHash == p + "Hash");
				});
			return userManager;
		}
	}
}
