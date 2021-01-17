using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Requests.Users.DeleteUser
{
    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Response<UserDTO>>
    {
        private readonly IUsersService _usersService;
        public DeleteUserRequestHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public async Task<Response<UserDTO>> Handle(
           DeleteUserRequest request, CancellationToken cancellationToken)
           => await _usersService.DeleteAsync(request.Id);
    }
}
