using Domain;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Repostiory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
	public interface IUserService
	{
		Task RegisterUser(AppUser model, string password);
		Task<AppUser> Login(string username, string password);
		Task Subscribe(AppUser source, AppUser target);
		Task Subscribe(string sourceId, string targetId);
		Task Unsubscribe(AppUser source, AppUser target);
		Task Unsubscribe(string sourceId, string targetId);
		Task ChangeUserProfilePic(string userId, Image newPic);
		Task ChangeUserProfilePic(AppUser userId, Image newPic);
	}

	public class UserService: IUserService
	{
		private readonly IRepository<AppUser, string> users;
		private readonly IRepository<Image, string> images;
		private readonly IRepository<Subscription, string> subscriptions;
		UserManager<AppUser> userManager;
		public UserService(IRepository<AppUser, string> users, IRepository<Subscription, string> subscriptions,
			IRepository<Image, string> images, UserManager<AppUser> userManager)
		{
			this.users = users;
			this.images	= images;
			this.subscriptions = subscriptions;
			this.userManager = userManager;
		}

		public async Task ChangeUserProfilePic(string userId, Image newPic)
		{
			var user = await users.Get(userId);
			await ChangeUserProfilePic(user, newPic);
		}

		public async Task ChangeUserProfilePic(AppUser user, Image newPic)
		{
			if (user == null)
				throw new UserNotFoundException();
			user.ProfilePic = newPic;
			await users.SaveChanges();
		}

		public async Task<AppUser> Login(string username, string password)
		{
			var user = await userManager.FindByNameAsync(username);
			if (user != null && await userManager.CheckPasswordAsync(user, password))
				return user;
			throw new BadCredentialsException(username);
		}

		public async Task RegisterUser(AppUser model, string password)
		{
			var result = await userManager.CreateAsync(model, password);
			if (!result.Succeeded)
				throw new RegisterException(result.Errors.Select(x => x.Description));
		}

		public async Task Subscribe(AppUser source, AppUser target)
		{
			await Subscribe(source.Id, target.Id);
		}

		public async Task Subscribe(string sourceId, string targetId)
		{
			if (users.Get(sourceId) == null || users.Get(targetId) == null)
				throw new UserNotFoundException();
			await subscriptions.Insert(new Subscription()
			{
				SubscriberId = sourceId,
				SubTargetId = targetId
			});
		}

		public Task Unsubscribe(AppUser source, AppUser target)
		{
			throw new NotImplementedException();
		}

		public Task Unsubscribe(string sourceId, string targetId)
		{
			throw new NotImplementedException();
		}
	}
}
