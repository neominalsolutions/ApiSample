using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcClient.Extensions
{
    public static class HttpContextExtensions
    {
        public async static Task JwtSignInAsync(this HttpContext httpContext,string authScheme,JwtSecurityToken jwtToken, string accessToken, string refreshToken, bool isPersistant = false)
        {
            int expireDateSeconds = (int)jwtToken.Payload.Exp;
            var claimPrinciple = new ClaimsPrincipal();

            var identity = new ClaimsIdentity(jwtToken.Payload.Claims, authScheme);
            claimPrinciple.AddIdentity(identity);


            var authProperties = new AuthenticationProperties();
            authProperties.IsPersistent = isPersistant;
            authProperties.ExpiresUtc = DateTimeOffset.FromUnixTimeSeconds(expireDateSeconds);

            var accessTokenInstance = new AuthenticationToken();
            accessTokenInstance.Name = "AccessToken";
            accessTokenInstance.Value = accessToken;

            var refreshTokenInstance = new AuthenticationToken();
            refreshTokenInstance.Name = "RefreshToken";
            refreshTokenInstance.Value = refreshToken;

            var tokens = new List<AuthenticationToken>();
            tokens.Add(accessTokenInstance);
            tokens.Add(refreshTokenInstance);

            authProperties.StoreTokens(tokens);


            await httpContext.SignInAsync(authScheme, claimPrinciple, authProperties);

            await Task.CompletedTask;
        }
    }
}
