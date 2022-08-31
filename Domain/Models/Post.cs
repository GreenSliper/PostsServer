using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
	public class Post : CrUpStampEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public string Id { get; set; }
		public string AuthorId { get; set; }
		public virtual AppUser Author { get; set; }
		public string Text { get; set; }
	}
}
