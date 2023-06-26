using System.Security.Claims;

namespace VolunteeringPlatform.API.Infrastructure.Extentions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            return loggedInUserId != null ? (int)Convert.ChangeType(loggedInUserId, typeof(int)) : (int)Convert.ChangeType(0, typeof(int));
        }
    }
}
