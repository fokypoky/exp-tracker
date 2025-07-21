namespace ExpTracker.Core.Models
{
	public enum ResponseResult
	{
		Ok = 200,
		BadRequest = 400,
		Unauthorized = 401,
		Forbidden = 403,
		NotFound = 404,
		InternalError = 500
	}
}
