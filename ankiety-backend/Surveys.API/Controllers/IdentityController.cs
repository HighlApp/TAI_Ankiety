using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Surveys.Infrastructure.DTO;
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
        public async Task<AuthResponseDTO> SignIn(SignInRequest request,
            CancellationToken cancellationToken)
            => await _mediator.Send(request, cancellationToken);

        //POST: api/identity/sign-up
        [HttpPost("sign-up")]
        public async Task<ActionResult> SingUp(SignUpRequest request,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        //POST: api/identity/change-password
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(
            ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        //POST: api/identity/refresh-token
        [HttpPost("refresh-token")]
        public async Task<AuthResponseDTO> RefreshToken(RefreshTokenRequest request,
            CancellationToken cancellationToken)
            => await _mediator.Send(request, cancellationToken);
    }
}
