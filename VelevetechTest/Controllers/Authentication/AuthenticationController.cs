using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VelevetechTest.Controllers.Authentication.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Services.Authentication.Contracts;
using VelevetechTest.ConfigurationModels;
using Microsoft.Extensions.Options;

namespace VelevetechTest.Controllers.Authentication
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IOptions<AppSettings> appSettings, IAuthenticationService authenticationService, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<LoginResult> Login([FromBody] LoginContract contract)
        {
            var refreshToken = Guid.NewGuid();

            var loginServiceContract = _mapper.Map<LoginServiceContract>(contract);

            loginServiceContract.RefreshToken = refreshToken;

            var loginServiceResult = await _authenticationService.Login(loginServiceContract);

            string encodedJwt = null;

            var now = DateTime.UtcNow;

            if (loginServiceResult.Success)
            {
 
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(contract.Login, loginServiceResult.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                encodedJwt = tokenHandler.WriteToken(token);
            }

            return await Task.FromResult(new LoginResult
            {
                Login = contract.Login,
                UserId = loginServiceResult.UserId,
                UserName = loginServiceResult.UserName,
                GiveinPlaceId = contract.GiveinPlaceId,
                AccessToken = encodedJwt,
                RefreshToken = refreshToken,
                ExpirationTime = now
                    .AddMinutes(30)
                    .AddSeconds(-10), //чтобы из-за задержек не получалось так, что токен уже просрочился, но ещё не обновился.
                IdleTimeout = 30,
                Error = loginServiceResult.Error,
            });
        }

        [HttpPost("logout")]
        public void Logout()
        {
            if (Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
            {
                _authenticationService.Logout(userId);
            }
        }

        [AllowAnonymous]
        [HttpPost("refreshtoken")]
        public RefreshTokenResult RefreshToken([FromBody] RefreshTokenContract contract)
        {
            ClaimsPrincipal сlaimsPrincipal = null;
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("authentification_security_key!qwe123"));
            try
            {
                сlaimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(contract.AccessToken, new TokenValidationParameters
                {
                    ValidIssuer = "VelevetechTestIssuer",
                    ValidAudience = "VelevetechTest",
                    IssuerSigningKey = key,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                }, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
            }
            catch
            {
                // invalid token/signing key was passed and we can't extract user claims
                return null;
            }

            var userIdStr = сlaimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var login = сlaimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;

            if (!Guid.TryParse(userIdStr, out Guid userId))
            {
                return null;
            }

            var newRefreshToken = Guid.NewGuid();
            _authenticationService.UpdateRefreshToken(userId, contract.RefreshToken, newRefreshToken);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };
            var identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: "VelevetechTestIssuer",
                    audience: "VelevetechTest",
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(30)),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new RefreshTokenResult
            {
                AccessToken = encodedJwt,
                RefreshToken = newRefreshToken,
                ExpirationTime = DateTime.Now.AddMinutes(30),
            };
        }
    }
}