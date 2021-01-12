using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.GetUserInvitations
{
    public class GetUserInvitationsRequest : IRequest<Response<IEnumerable<UserInvitationDTO>>>
    {
        public GetUserInvitationsRequest(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
