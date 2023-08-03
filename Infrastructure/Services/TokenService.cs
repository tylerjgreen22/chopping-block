using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    // Service class for generating JWT tokens
    public class TokenService : ITokenService
    {
        // Private readonly fields for the configuration and for generating a Symmetric Security Key
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        // Constructor retrieves configuration via dependency injection, and initialize key field by creating a new key using the key string from config
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

        // Method for creating the token using an App User
        public string CreateToken(AppUser user)
        {
            // List of claims that will be added to the token, contains information about the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName)
            };

            // Creating the credentials using the key and the HmacSha512Signature that will be used to sign the key
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Creating descriptor that contains the claims, expiration timer, signing credentials and issuer from config
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
            };

            // Creating handler that generates JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            // Creating the token using the handler with the descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Returning JWT written to a string after serialization
            return tokenHandler.WriteToken(token);
        }
    }
}