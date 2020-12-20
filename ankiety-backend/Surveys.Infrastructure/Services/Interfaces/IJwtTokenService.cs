using Surveys.Core.Entities;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;

namespace Surveys.Infrastructure.Services.Interfaces
{
    public interface IJwtTokenService
    {
        Task<AuthResponseDTO> GenerateJwtTokenWithRefreshToken(User user);

        Task<AuthResponseDTO> RefreshTokenAsync(string token, string refreshToken);

        Task InvalidateUserRefreshTokensAsync(string userId);
    }
}
