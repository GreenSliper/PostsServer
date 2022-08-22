using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public override string Message => message;

		string message;
		public UserNotFoundException(string message = "User not found in database")
		{
			this.message = message;
		}
	}
}
