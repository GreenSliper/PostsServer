using Domain.Exceptions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using Domain.Exceptions;

namespace PostsServerCore3.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		//private readonly ILoggerManager _logger;
		public ExceptionMiddleware(RequestDelegate next/*, ILoggerManager logger*/)
		{
			//_logger = logger;
			_next = next;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				//_logger.LogError($"Something went wrong: {ex}");
				await HandleExceptionAsync(httpContext, ex);
			}
		}
		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			string message = exception.Message;
			switch (exception)
			{
				case BadCredentialsException bce:
					message = bce.Message;
					context.Response.StatusCode = 400;
					break;
				case UserNotFoundException unfe:
					message = unfe.Message;
					context.Response.StatusCode = 404;
					break;
				default: 
					break;
			}
			await context.Response.WriteAsync(new ErrorDetails()
			{
				StatusCode = context.Response.StatusCode,
				Message = message
			}.ToString());
		}
	}
}
