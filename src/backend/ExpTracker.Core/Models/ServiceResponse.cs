namespace ExpTracker.Core.Models
{
	public class ServiceResponse<T>
	{
		public ResponseResult Result { get; set; }
		public T? Data { get; set; }
		public string? Error { get; set; }

		public static ServiceResponse<T> Ok(T entity)
		{
			return new ServiceResponse<T>()
			{
				Result = ResponseResult.Ok,
				Data = entity
			};
		}

		public static ServiceResponse<T> NotFound(string entity)
		{
			return new ServiceResponse<T>()
			{
				Result = ResponseResult.NotFound,
				Error = $"{entity} not found"
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
	}
}
