using MediatR;
using Surveys.Infrastructure.DTO;

namespace Surveys.Infrastructure.Requests.Identity.RefreshToken
{
    public class RefreshTokenRequest : IRequest<AuthResponseDTO>
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
