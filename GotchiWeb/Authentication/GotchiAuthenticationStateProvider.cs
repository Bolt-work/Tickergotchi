using Gotchi;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace GotchiWeb.Authentication
{
    public class GotchiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public GotchiAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var builtClaimsPrincipal = BuildClaimsPrincipal(userSession);

                // Check if anonymous
                if (!builtClaimsPrincipal.Claims.Any())
                    return await Task.FromResult(new AuthenticationState(_anonymous));


                var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(builtClaimsPrincipal.Claims, "GotchiAuth"));
                return await Task.FromResult(new AuthenticationState(claimPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserSession userSession) 
        {
            ClaimsPrincipal claimsPrincipal = BuildClaimsPrincipal(userSession);

            if (!claimsPrincipal.Claims.Any())
                return;

            await _sessionStorage.SetAsync("UserSession", userSession);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task LogoutAuthentication() 
        {
            await _sessionStorage.DeleteAsync("UserSession");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private ClaimsPrincipal BuildClaimsPrincipal(UserSession? userSession) 
        {
            if (userSession is null)
                return _anonymous;

            if(userSession.SerialNumber is null)
                return _anonymous;

            if (userSession.UserName is null)
                return _anonymous;

            if (userSession.Role is null)
                return _anonymous;

            return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.SerialNumber, userSession.SerialNumber),
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }));
        }
    }
}
