using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.GetInvitations
{
    public class GetInvitationsRequestHandler :
        IRequestHandler<GetInvitationsRequest, Response<IEnumerable<AdministratorInvitationDTO>>>
    {
        public Task<Response<IEnumerable<AdministratorInvitationDTO>>> Handle(
            GetInvitationsRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
