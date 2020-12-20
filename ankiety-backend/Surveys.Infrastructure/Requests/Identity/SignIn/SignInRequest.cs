using MediatR;
using Surveys.Infrastructure.DTO;

namespace Surveys.Infrastructure.Requests.Identity.SignIn
{
    public class SignInRequest : IRequest<AuthResponseDTO>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
