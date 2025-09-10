using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ExpTracker.Api.Models;

namespace ExpTracker.Api.Middlewares
{
	public class ExceptionsMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionsMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				httpContext.Response.ContentType = "application/json";
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
				{
					Code = 500,
					Message = "Internal server error occured"
				});
			}
		}
	}
}
