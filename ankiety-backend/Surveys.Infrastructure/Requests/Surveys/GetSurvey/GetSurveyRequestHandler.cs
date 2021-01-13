using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Surveys.GetSurvey
{
    public class GetSurveyRequestHandler : IRequestHandler<GetSurveyRequest, Response<SurveyDTO>>
    {
        private readonly ISurveysService _surveysService;

        public GetSurveyRequestHandler(ISurveysService surveysService)
        {
            _surveysService = surveysService;
        }

        public async Task<Response<SurveyDTO>> Handle(
            GetSurveyRequest request, CancellationToken cancellationToken)
            => await _surveysService.GetByIdAsync(request.Id);
    }
}
