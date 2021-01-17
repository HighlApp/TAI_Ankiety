using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Requests.Users.UpdateUser
{
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, Response<UserDTO>>
    {
        private readonly IUsersService _usersService;
        public UpdateUserRequestHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public async Task<Response<UserDTO>> Handle(
           UpdateUserRequest request, CancellationToken cancellationToken)
           => await _usersService.UpdateAsync(request);
    }
}

