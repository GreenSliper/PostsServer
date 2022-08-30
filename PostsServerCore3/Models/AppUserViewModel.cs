using AutoMapper;
using Domain.Models;
using System.Linq;

namespace PostsServerCore3.Models
{
	public class AppUserViewModel
	{
		public ImageVM ProfilePic { get; set; }
		public string UserName { get; set; }
		public string ProfileInfo { get; set; }
		public string Id { get; set; }
		public int SubscribersCount { get; set; }
		public int SubscribtionsCount { get; set; }
		public int PostsCount { get; set; } = 0;
	}
	public class SubscribersResolver : IValueResolver<AppUser, AppUserViewModel, int>
	{
		public int Resolve(AppUser source, AppUserViewModel destination, int member, ResolutionContext context)
		{
			return source.Subscribers.Count();
		}
	}
	public class SubscriptionsResolver : IValueResolver<AppUser, AppUserViewModel, int>
	{
		public int Resolve(AppUser source, AppUserViewModel destination, int member, ResolutionContext context)
		{
			return source.Subscriptions.Count();
		}
	}
}
