using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Models
{
	public class AppUser : IdentityUser
	{
		public virtual Image ProfilePic { get; set; }
		public string ProfileInfo { get; set; }
		public virtual ICollection<Subscription> Subscribers { get; set; }
		public virtual ICollection<Subscription> Subscriptions { get; set; }
	}
}
