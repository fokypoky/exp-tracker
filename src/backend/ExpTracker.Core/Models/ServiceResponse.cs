using ExpTracker.DataAccess.PostgreSQL.Models;

namespace ExpTracker.Core.Models
{
	public class ServiceResponse<T>
	{
		public ResponseResult Result { get; set; }
		public T? Data { get; set; }
		public string? Error { get; set; }

		public bool IsSuccess()
		{
			return Result == ResponseResult.Ok || Result == ResponseResult.Partial || Result == ResponseResult.Created;
		}

		public static ServiceResponse<T> Created(T entity)
		{
			return new ServiceResponse<T>()
			{
				Result = ResponseResult.Created,
				Data = entity
			};
		}

		public static ServiceResponse<T> Unauthorized(string errorMessage)
		{
			return new ServiceResponse<T>()
			{
				Result = ResponseResult.Unauthorized,
				Error = errorMessage
			};
		}

		public static ServiceResponse<T> Ok(T entity)
		{
			return new ServiceResponse<T>()
			{
				Result = ResponseResult.Ok,
				Data = entity
			};
		}

		public static ServiceResponse<T> NotFound(string message)
		{
			return new ServiceResponse<T>()
			{
				Result = ResponseResult.NotFound,
				Error = message,
			};
		}

		public static ServiceResponse<T> BadRequest(string message)
		{
			return new ServiceResponse<T>()
			{
				Result = ResponseResult.BadRequest,
				Error = message
			};
		}

		public static ServiceResponse<PaginatedResponse<T>> Partial(List<T> collection, int totalCount)
		{
			return new ServiceResponse<PaginatedResponse<T>>
			{
				Result = ResponseResult.Partial,
				Data = new PaginatedResponse<T>
				{
					Data = collection,
					TotalCount = totalCount,
				}
			};
		}
	}
}
