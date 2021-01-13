using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.GetUsersForInvitation
{
    public class GetUsersForInvitationRequest : IRequest<Response<IEnumerable<UserDTO>>>
    {
        public GetUsersForInvitationRequest(Guid surveyId)
        {
            SurveyId = surveyId;
        }

        public Guid SurveyId { get; set; }
    }
}
