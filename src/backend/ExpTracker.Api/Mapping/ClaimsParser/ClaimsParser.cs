using System.Security.Claims;

namespace ExpTracker.Api.Mapping.ClaimsParser
{
    public static class ClaimsParser
    {
        public static string GetUser(ClaimsPrincipal claims)
        {
            var user = claims.Claims.FirstOrDefault(c => c.Type == "login")?.Value;

            if (user == null) throw new ArgumentNullException("Login JWT token claim not found");
            
            return user;
        }
    }
}