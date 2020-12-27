using Surveys.Core.Entities;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Services.Interfaces
{
    public interface IJwtTokenService
    {
        Task<AuthResponseDTO> GenerateJwtTokenWithRefreshToken(User user);

        Task<Response<AuthResponseDTO>> RefreshTokenAsync(string token, string refreshToken);

        Task InvalidateUserRefreshTokensAsync(string userId);
    }
}
