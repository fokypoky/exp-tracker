namespace ExpTracker.Core.Models
{
	public enum ResponseResult
	{
		Ok = 200,
		Created = 201,
		Partial = 206,
		BadRequest = 400,
		Unauthorized = 401,
		Forbidden = 403,
		NotFound = 404,
		InternalError = 500
	}
}
