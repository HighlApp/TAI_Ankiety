using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Invitations.GetInvitations
{
    public class GetInvitationsRequestHandler :
        IRequestHandler<GetInvitationsRequest, Response<IEnumerable<AdministratorInvitationDTO>>>
    {
        private readonly IInvitationsService _invitationsService;

        public GetInvitationsRequestHandler(IInvitationsService invitationsService)
        {
            _invitationsService = invitationsService;
        }

        public async Task<Response<IEnumerable<AdministratorInvitationDTO>>> Handle(
            GetInvitationsRequest request, CancellationToken cancellationToken)
            => await _invitationsService.GetAllInvitationsAsync();
    }
}
