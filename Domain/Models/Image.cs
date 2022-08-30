using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
	public class Image
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public string Id { get; set; }
		public byte[] Bytes { get; set; }
	}
}
