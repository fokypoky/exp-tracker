using ExpTracker.Api.Models;
using ExpTracker.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Mapping.ResponseMapper
{
	public static class ResponseMapper
	{
		private static ErrorResponse MapError<T>(ServiceResponse<T> response)
		{
			return new ErrorResponse() { Code = (int)response.Result, Message = response.Error ?? "Error text is empty" };
		}

		// TODO: Маппить 403 forbidden код
		public static IResult MapResponse<T>(ServiceResponse<T> response)
		{
			return response.Result switch
			{
				ResponseResult.Ok => Results.Ok(response.Data),
				ResponseResult.Created => Results.Created(),
				ResponseResult.BadRequest => Results.BadRequest(MapError(response)),
				ResponseResult.NotFound => Results.NotFound(MapError(response)),
				ResponseResult.Unauthorized => Results.Unauthorized(),
				ResponseResult.InternalError => Results.InternalServerError(MapError(response)),
				_ => throw new ArgumentException("Unknown result")
			};
		}
	}
}
