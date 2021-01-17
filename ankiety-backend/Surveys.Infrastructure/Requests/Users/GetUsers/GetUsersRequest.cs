using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Requests.Users.GetUsers
{
    public class GetUsersRequest : IRequest<Response<IEnumerable<UserDTO>>>
    {
    }
}
