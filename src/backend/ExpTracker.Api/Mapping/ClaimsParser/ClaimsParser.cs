using System.Security.Claims;

namespace ExpTracker.Api.Mapping.ClaimsParser
{
    public static class ClaimsParser
    {
        private static string GetClaimErrorMessage(string claimName) => $"{claimName} in JWT token claim not found";

        public static string GetUser(ClaimsPrincipal claims)
        {
            var user = claims.Claims.FirstOrDefault(c => c.Type == "login")?.Value;

            if (user == null) throw new ArgumentNullException(GetClaimErrorMessage("Login"));
            
            return user;
        }

        public static Guid GetUserId(ClaimsPrincipal claims)
        {
            var guid = claims.Claims.FirstOrDefault(c => c.Type == "guid")?.Value;

            if (guid == null) throw new ArgumentNullException(GetClaimErrorMessage("Guid"));

            return Guid.Parse(guid);
        }
    }
}