using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.DeleteInvitation
{
    public class DeleteInvitationRequest : IRequest<Response<StatusResponseDTO>>
    {
        public DeleteInvitationRequest(int invitationId)
        {
            InvitationId = invitationId;
        }

        public int InvitationId { get; set; }
    }
}
