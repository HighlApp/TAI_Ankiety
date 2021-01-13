using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Surveys.PostSurvey
{
    class PostSurveyRequestHandler : IRequestHandler<PostSurveyRequest, Response<SurveyDTO>>
    {
        private readonly ISurveysService _surveysService;

        public PostSurveyRequestHandler(ISurveysService surveysService)
        {
            _surveysService = surveysService;
        }

        public async Task<Response<SurveyDTO>> Handle(
            PostSurveyRequest request, CancellationToken cancellationToken)
            => await _surveysService.PostAsync(request);
    }
}
