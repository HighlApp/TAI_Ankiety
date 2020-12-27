using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Identity.SignUp
{ 
    public class SignUpRequestHandler : 
        IRequestHandler<SignUpRequest, Response<StatusResponseDTO>>
    {
        private readonly IIdentityService _identityService;

        public SignUpRequestHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Response<StatusResponseDTO>> Handle(SignUpRequest request, 
            CancellationToken cancellationToken)
            => await _identityService.SignUpAsync(
                request.Name, request.Surname, request.Email, request.Password);
    }
}
