using DevPortfolioAPI.Models;

namespace DevPortfolioAPI.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
