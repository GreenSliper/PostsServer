using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
	public class CrStampEntity
	{
		public DateTime CreatedTime { get; set; }
	}

	public class CrUpStampEntity : CrStampEntity
	{
		public DateTime UpdatedTime { get; set; }
	}
}
