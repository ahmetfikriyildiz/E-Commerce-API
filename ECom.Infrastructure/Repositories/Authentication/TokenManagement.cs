using ECom.Domain.Entities.Identity;
using ECom.Domain.Interfaces.Authentication;
using ECom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECom.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(AppDbContext context , IConfiguration configure) : ITokenManagement
    {
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            context.RefreshTokens.Add(new RefreshToken { UserId = userId, Token = refreshToken });
            return await context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)
        {
           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configure["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configure["Jwt:Issuer"],
                audience: configure["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomBytes = new byte[byteSize];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
                return Convert.ToBase64String(randomBytes);
        }

        public List<Claim> GetUserClaims(string token)
        {
           var tokenHandler = new JwtSecurityTokenHandler();
           var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken != null)
            return jwtToken.Claims.ToList();
            else
            return [];
        }

        public async Task<string> GetUserIdByRefreshToken(string refreshToken) 
            => (await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken))!.UserId;

        public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
            if (user == null) return -1;
            user.Token = refreshToken;
            return await context.SaveChangesAsync();
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
            return user != null;
        }
    }
}
