using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
	public class Subscription
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public string SubscriptionId { get; set; }
		public string SubTargetId { get; set; }
		public virtual AppUser SubTarget { get; set; }
		public string SubscriberId{get; set; }
		public virtual AppUser Subscriber { get; set; }
	}
}
