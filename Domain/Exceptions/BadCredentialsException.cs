using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
	public class BadCredentialsException : Exception
	{
		string username;
		public override string Message =>
			$"Bad credentials: check login & password! Login: {username ?? "unknown"}";
		public BadCredentialsException(string username = null) 
		{
			this.username = username;
		}
	}
}
