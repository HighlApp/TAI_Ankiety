using MediatR;

namespace Surveys.Infrastructure.Requests.Identity.SignUp
{
    public class SignUpRequest : IRequest
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
