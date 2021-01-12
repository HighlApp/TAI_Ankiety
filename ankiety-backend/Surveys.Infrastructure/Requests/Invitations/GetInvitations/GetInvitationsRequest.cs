using MediatR;
using System.Collections.Generic;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.GetInvitations
{
    public class GetInvitationsRequest: IRequest<Response<IEnumerable<AdministratorInvitationDTO>>>
    {
    }
}
