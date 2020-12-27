using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Identity.SignIn
{
    public class SignInRequestHandler :
        IRequestHandler<SignInRequest, Response<AuthResponseDTO>>
    {
        private readonly IIdentityService _identityService;

        public SignInRequestHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Response<AuthResponseDTO>> Handle(SignInRequest request, 
            CancellationToken cancellationToken)
            => await _identityService.SignInAsync(request.Email, request.Password); 
    }
}
