using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Requests.Users.GetUsers
{
    public class GetUsersRequestHandler :
         IRequestHandler<GetUsersRequest, Response<IEnumerable<UserDTO>>>
    {

        private readonly IUsersService _usersService;

        public GetUsersRequestHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<Response<IEnumerable<UserDTO>>> Handle(
            GetUsersRequest request, CancellationToken cancellationToken)
            =>  await _usersService.GetUsersAsync();
    }
}
