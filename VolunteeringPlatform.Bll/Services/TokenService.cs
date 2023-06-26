using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VolunteeringPlatform.Bll.Configurations;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Bll.Services
{
    public class TokenService : ITokenService
    {
        private readonly AuthOptions _authenticationOptions;
        private readonly UserManager<User> _userManager;

        public TokenService(IOptions<AuthOptions> authenticationOptions, UserManager<User> userManager)
        {
            _authenticationOptions = authenticationOptions.Value;
            _userManager = userManager;
        }

        public async Task<string> GenerateAccessToken(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var userId = user.Id.ToString();
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, userId) };

            foreach(var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var signinCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                 issuer: _authenticationOptions.Issuer,
                 audience: _authenticationOptions.Audience,
                 claims: claims,
                 expires: DateTime.Now.AddDays(30),
                 signingCredentials: signinCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);
            return encodedToken;
        }
    }
}
