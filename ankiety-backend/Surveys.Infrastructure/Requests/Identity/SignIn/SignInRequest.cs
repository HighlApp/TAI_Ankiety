using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Identity.SignIn
{
    public class SignInRequest : IRequest<Response<AuthResponseDTO>>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
