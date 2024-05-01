using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KropkaNetApi.Middleware
{
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _issuer;
        private readonly string _key;

        public JwtAuthorizationMiddleware(RequestDelegate next, string issuer, string key)
        {
            _next = next;
            _issuer = issuer;
            _key = key;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Cookies.TryGetValue("jwtToken", out string token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var principal = await ValidateToken(token, context);
            if (principal == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            context.User = principal; // Optional: Set user principal for access in controllers
            await _next(context);
        }

        private async Task<ClaimsPrincipal> ValidateToken(string token, HttpContext httpContext)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _issuer,
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }
    }
}