using System.Security.Claims;

namespace ECom.Domain.Interfaces.Authentication
{
    public interface ITokenManagement
    {
        string GetRefreshToken();
        List<Claim> GetUserClaims(string token);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<string> GetUserIdByRefreshToken(string refreshToken);
        Task<int> AddRefreshToken(string userId , string refreshToken);
        Task<int> UpdateRefreshToken(string userId , string refreshToken);
        string GenerateToken(List<Claim> claims);
    }
}
