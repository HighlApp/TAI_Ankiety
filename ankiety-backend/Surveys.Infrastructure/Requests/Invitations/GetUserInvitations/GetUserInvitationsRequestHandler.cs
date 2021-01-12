using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.GetUserInvitations
{
    public class GetUserInvitationsRequestHandler :
        IRequestHandler<GetUserInvitationsRequest, Response<IEnumerable<UserInvitationDTO>>>
    {
        public Task<Response<IEnumerable<UserInvitationDTO>>> Handle(GetUserInvitationsRequest request, 
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
