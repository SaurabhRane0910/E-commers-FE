using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace blazornew.Service
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_anonymous));
        }

        public async Task NotifyUserLoggedIn(string token)
        {
            // Create claims from token if needed
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, "User")
        }, "jwtAuthType");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}