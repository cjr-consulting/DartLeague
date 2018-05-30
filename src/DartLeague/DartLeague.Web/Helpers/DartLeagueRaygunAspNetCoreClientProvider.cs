using Microsoft.AspNetCore.Http;
using Mindscape.Raygun4Net;
using System.Linq;
using System.Security.Claims;
using Mindscape.Raygun4Net.AspNetCore;

namespace DartLeague.Web.Helpers
{
    public class DartLeagueRaygunAspNetCoreClientProvider : DefaultRaygunAspNetCoreClientProvider
    {
        public override RaygunClient GetClient(RaygunSettings settings, HttpContext context)
        {
            var client = base.GetClient(settings, context);

            var identity = context?.User?.Identity as ClaimsIdentity;
            if (identity?.IsAuthenticated == true)
            {
                var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();

                client.UserInfo = new RaygunIdentifierMessage(email)
                {
                    IsAnonymous = false,
                    Email = email,
                    FullName = identity.Name
                };
            }

            return client;
        }
    }
}
