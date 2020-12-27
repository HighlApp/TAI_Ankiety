using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Surveys.Infrastructure.Requests.Identity.SignIn;
using Surveys.Infrastructure.Requests.Identity.SignUp;
using Surveys.Infrastructure.Requests.Identity.RefreshToken;
using Surveys.Infrastructure.Requests.Identity.ChangePassword;

namespace Surveys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //POST: api/identity/sign-in
        [HttpPost("sign-in")]
        public async Task<Response<AuthResponseDTO>> SignIn(
            SignInRequest request, CancellationToken cancellationToken)
            => await _mediator.Send(request, cancellationToken);

        //POST: api/identity/sign-up
        [HttpPost("sign-up")]
        public async Task<Response<StatusResponseDTO>> SingUp(
            SignUpRequest request, CancellationToken cancellationToken)
            => await _mediator.Send(request, cancellationToken);

        //POST: api/identity/change-password
        [Authorize]
        [HttpPost("change-password")]
        public async Task<Response<StatusResponseDTO>> ChangePassword(
            ChangePasswordRequest request, CancellationToken cancellationToken)
            => await _mediator.Send(request, cancellationToken);

        //POST: api/identity/refresh-token
        [HttpPost("refresh-token")]
        public async Task<Response<AuthResponseDTO>> RefreshToken(
            RefreshTokenRequest request, CancellationToken cancellationToken)
            => await _mediator.Send(request, cancellationToken);
    }
}
