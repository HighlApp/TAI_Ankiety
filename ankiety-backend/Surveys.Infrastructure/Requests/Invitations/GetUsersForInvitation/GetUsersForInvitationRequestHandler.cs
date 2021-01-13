using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Invitations.GetUsersForInvitation
{
    public class GetUsersForInvitationRequestHandler :
        IRequestHandler<GetUsersForInvitationRequest, Response<IEnumerable<UserDTO>>>
    {
        private readonly IInvitationsService _invitationsService;

        public GetUsersForInvitationRequestHandler(IInvitationsService invitationsService)
        {
            _invitationsService = invitationsService;
        }

        public async Task<Response<IEnumerable<UserDTO>>> Handle(
            GetUsersForInvitationRequest request, CancellationToken cancellationToken)
            => await _invitationsService.GetUsersForInvitationAsync(request.SurveyId);
    }
}
