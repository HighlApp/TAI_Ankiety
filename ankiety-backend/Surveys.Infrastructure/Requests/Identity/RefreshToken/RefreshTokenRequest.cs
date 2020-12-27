using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Identity.RefreshToken
{
    public class RefreshTokenRequest : IRequest<Response<AuthResponseDTO>>
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
