using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.GetFilledSurvey
{
    public class GetFilledSurveyRequest : IRequest<Response<FilledSurveyDTO>>
    {
        public GetFilledSurveyRequest(int invitationId)
        {
            InvitationId = invitationId;
        }

        public int InvitationId { get; set; }
    }
}
