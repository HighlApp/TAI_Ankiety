using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Identity.SignIn
{
    public class SignInRequestHandler :
        IRequestHandler<SignInRequest, AuthResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public SignInRequestHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthResponseDTO> Handle(SignInRequest request, 
            CancellationToken cancellationToken)
            => await _identityService.SignInAsync(request.Email, request.Password); 
    }
}
