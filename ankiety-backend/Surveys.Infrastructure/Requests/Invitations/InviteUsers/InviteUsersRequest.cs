using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.InviteUsers
{
    public class InviteUsersRequest : IRequest<Response<StatusResponseDTO>>
    {
        public InvitationDetailsDTO InvitationDetails { get; set; }

        public IEnumerable<Guid> UsersId { get; set; }
    }
}
