using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Identity.RefreshToken
{
    public class RefreshTokenRequestHandler :
        IRequestHandler<RefreshTokenRequest, AuthResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public RefreshTokenRequestHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public Task<AuthResponseDTO> Handle(RefreshTokenRequest request,
            CancellationToken cancellationToken)
            => _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);
    }
}
