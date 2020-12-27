using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Identity.ChangePassword
{
    public class ChangePasswordRequest : IRequest<Response<StatusResponseDTO>>
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }
    }
}
