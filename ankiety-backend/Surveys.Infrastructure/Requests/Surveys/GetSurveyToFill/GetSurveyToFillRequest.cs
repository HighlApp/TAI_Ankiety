﻿using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.GetSurveyToFill
{
    public class GetSurveyToFillRequest : IRequest<Response<SurveyToFillDTO>>
    {
        public GetSurveyToFillRequest(int invitationId)
        {
            InvitationId = invitationId;
        }

        public int InvitationId { get; set; }
    }
}
