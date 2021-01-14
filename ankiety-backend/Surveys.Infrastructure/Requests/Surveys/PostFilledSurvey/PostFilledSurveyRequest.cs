using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.PostFilledSurvey
{
    public class PostFilledSurveyRequest : IRequest<Response<StatusResponseDTO>>
    {
        public FilledSurveyDTO FilledSurvey { get; set; }
    }
}
