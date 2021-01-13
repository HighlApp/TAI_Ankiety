using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.DeleteInvitation
{
    public class DeleteInvitationRequest : IRequest<Response<StatusResponseDTO>>
    {
        public DeleteInvitationRequest(Guid invitationId)
        {
            InvitationId = invitationId;
        }

        public Guid InvitationId { get; set; }
    }
}
