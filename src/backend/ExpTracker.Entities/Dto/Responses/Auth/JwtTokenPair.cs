namespace ExpTracker.Entities.Dto.Responses.Auth
{
	public class JwtTokenPair
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
