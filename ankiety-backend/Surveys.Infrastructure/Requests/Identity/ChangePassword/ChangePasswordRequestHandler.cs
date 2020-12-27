using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Identity.ChangePassword
{
    public class ChangePasswordRequestHandler :
        IRequestHandler<ChangePasswordRequest, Response<StatusResponseDTO>>
    {
        private readonly IIdentityService _identityService;

        public ChangePasswordRequestHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Response<StatusResponseDTO>> Handle(
            ChangePasswordRequest request, CancellationToken cancellationToken)
            => await _identityService.ChangePasswordAsync(
                request.Password, request.NewPassword);
    }
}
