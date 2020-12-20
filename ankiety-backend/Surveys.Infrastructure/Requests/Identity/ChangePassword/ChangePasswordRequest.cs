using MediatR;

namespace Surveys.Infrastructure.Requests.Identity.ChangePassword
{
    public class ChangePasswordRequest : IRequest
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }
    }
}
