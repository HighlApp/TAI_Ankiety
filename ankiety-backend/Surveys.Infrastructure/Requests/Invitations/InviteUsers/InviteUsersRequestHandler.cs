using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Invitations.InviteUsers
{
    public class InviteUsersRequestHandler :
        IRequestHandler<InviteUsersRequest, Response<StatusResponseDTO>>
    {
        private readonly IInvitationsService _invitationsService;

        public InviteUsersRequestHandler(IInvitationsService invitationsService)
        {
            _invitationsService = invitationsService;
        }

        public async Task<Response<StatusResponseDTO>> Handle(
            InviteUsersRequest request, CancellationToken cancellationToken)
            => await _invitationsService.InviteUsersAsync(request.UsersId, request.InvitationDetails);
    }
}
