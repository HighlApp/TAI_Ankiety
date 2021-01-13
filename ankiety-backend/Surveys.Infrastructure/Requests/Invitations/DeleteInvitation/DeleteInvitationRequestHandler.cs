using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Invitations.DeleteInvitation
{
    public class DeleteInvitationRequestHandler :
        IRequestHandler<DeleteInvitationRequest, Response<StatusResponseDTO>>
    {
        private readonly IInvitationsService _invitationsService;

        public DeleteInvitationRequestHandler(IInvitationsService invitationsService)
        {
            _invitationsService = invitationsService;
        }

        public async Task<Response<StatusResponseDTO>> Handle(
            DeleteInvitationRequest request, CancellationToken cancellationToken)
            => await _invitationsService.DeleteInvitationAsync(request.InvitationId);
    }
}
