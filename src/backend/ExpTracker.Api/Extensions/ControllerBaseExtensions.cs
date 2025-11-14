using ExpTracker.Api.Models;
using ExpTracker.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpTracker.Api.Extensions
{
    public static class ControllerBaseExtensions
    {
        private static ErrorResponse MapError<T>(ServiceResponse<T> response)
        {
            return new ErrorResponse() { Code = (int)response.Result, Message = response.Error ?? "Error text is empty" };
        }

        public static IActionResult MapResponse<T>(this ControllerBase controller, ServiceResponse<T> response)
        {

            return response.Result switch
            {
                ResponseResult.Ok => controller.Ok(response.Data),
                ResponseResult.Created => controller.Created(),
                ResponseResult.Partial => throw new ArgumentException("Use MapPaginatedResponse<T> to map this response type"),
                ResponseResult.BadRequest => controller.BadRequest(MapError(response)),
                ResponseResult.NotFound => controller.NotFound(MapError(response)),
                ResponseResult.Unauthorized => controller.Unauthorized(MapError(response)),
                ResponseResult.InternalError => controller.StatusCode((int)HttpStatusCode.InternalServerError),
                _ => throw new ArgumentException("Unknown result code")
            };
        }

        public static IActionResult MapPaginatedResponse<T>(this ControllerBase controller, ServiceResponse<PaginatedResponse<T>> response)
        {
            if (!response.IsSuccess()) return controller.StatusCode((int)response.Result, MapError(response));

            controller.HttpContext.Response.Headers.Add("x-total-count", response.Data.TotalCount.ToString());

            return controller.StatusCode((int)HttpStatusCode.PartialContent, response.Data.Data);
        }
    }
}
