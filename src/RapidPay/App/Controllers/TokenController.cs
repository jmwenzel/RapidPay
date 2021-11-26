using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Infrastructure.Core.Common;
using RapidPay.Infrastructure.Core.Filters;
using RapidPay.Infrastructure.Core.Middleware;
using RapidPay.Infrastructure.Core.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RapidPay.App.Controllers
{
    /// <summary>
    /// JWT Token generator class protected by Basic Authentication
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route(Config.ROUTE_PREFFIX + "/v{version:apiVersion}/token")]
    public class TokenController : Controller
    {
        private readonly OAuthSettings _settings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        public TokenController(IOptions<OAuthSettings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Get JWT Token
        /// </summary>
        /// <returns></returns>
        [HttpGet, BasicAuthorization]
        public IActionResult Token()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var authenticatedUser = new AuthenticatedUser(JwtBearerDefaults.AuthenticationScheme, true, "rapidPayBearer");

            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authenticatedUser),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SymmetricSecurityKey)), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow
            }));

            return Ok(new { access_token = accessToken, token_type = "bearer", expires_in = 900 });
        }
    }
}
