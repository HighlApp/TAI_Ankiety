using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Invitations.GetUserInvitations
{
    public class GetUserInvitationsRequestHandler :
        IRequestHandler<GetUserInvitationsRequest, Response<IEnumerable<UserInvitationDTO>>>
    {

        private readonly IInvitationsService _invitationsService;

        public GetUserInvitationsRequestHandler(IInvitationsService invitationsService)
        {
            _invitationsService = invitationsService;
        }

        public async Task<Response<IEnumerable<UserInvitationDTO>>> Handle(
            GetUserInvitationsRequest request, CancellationToken cancellationToken)
            => await _invitationsService.GetUserInvitationsAsync();
    }
}
