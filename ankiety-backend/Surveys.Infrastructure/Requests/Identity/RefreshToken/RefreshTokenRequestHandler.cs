using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Identity.RefreshToken
{
    public class RefreshTokenRequestHandler :
        IRequestHandler<RefreshTokenRequest, Response<AuthResponseDTO>>
    {
        private readonly IJwtTokenService _jwtTokenService;

        public RefreshTokenRequestHandler(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        public async Task<Response<AuthResponseDTO>> Handle(
            RefreshTokenRequest request, CancellationToken cancellationToken)
            => await _jwtTokenService.RefreshTokenAsync(
                request.Token, request.RefreshToken);
    }
}
