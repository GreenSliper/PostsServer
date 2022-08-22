using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
	#nullable enable
	public class ResponseModel
	{
		public string? Status { get; set; }
		public string? Message { get; set; }
		public ResponseModel(string? message = null, string? status = null)
		{
			Status = status;
			Message = message;
		}
	}
	#nullable disable
}
