using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Surveys.PostFilledSurvey
{
    public class PostFilledSurveyRequestHandler :
        IRequestHandler<PostFilledSurveyRequest, Response<StatusResponseDTO>>
    {
        private readonly ISurveysService _surveysService;

        public PostFilledSurveyRequestHandler(ISurveysService surveysService)
        {
            _surveysService = surveysService;
        }

        public async Task<Response<StatusResponseDTO>> Handle(
            PostFilledSurveyRequest request, CancellationToken cancellationToken)
            => await _surveysService.SaveFilledSurvey(request.FilledSurvey);
    }
}
