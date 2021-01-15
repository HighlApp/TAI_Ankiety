using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.GetFilledSurvey
{
    public class GetFilledSurveyRequest : IRequest<Response<FilledSurveySummaryDTO>>
    {
        public GetFilledSurveyRequest(Guid invitationId)
        {
            InvitationId = invitationId;
        }

        public Guid InvitationId { get; set; }
    }
}
