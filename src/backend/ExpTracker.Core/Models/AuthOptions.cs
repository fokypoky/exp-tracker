namespace ExpTracker.Core.Models
{
	public class AuthOptions
	{
		public string Issuer { get; set; }
		public string AccessTokenSecret { get; set; }
		public string RefreshTokenSecret { get; set; }
		public int AccessTokenExpireMinutes { get; set; }
		public int RefreshTokenExpireDays { get; set; }
	}
}
