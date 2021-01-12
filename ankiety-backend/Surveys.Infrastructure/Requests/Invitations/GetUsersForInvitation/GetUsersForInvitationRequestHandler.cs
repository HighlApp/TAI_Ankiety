using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.GetUsersForInvitation
{
    public class GetUsersForInvitationRequestHandler :
        IRequestHandler<GetUsersForInvitationRequest, Response<IEnumerable<UserDTO>>>
    {
        public Task<Response<IEnumerable<UserDTO>>> Handle(GetUsersForInvitationRequest request, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
