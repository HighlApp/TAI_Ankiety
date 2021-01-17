using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Requests.Surveys.GetNumbersForSurvey
{
    public class GetNumbersForSurveyRequestHandler : IRequestHandler<GetNumbersForSurveyRequest, Response<SurveyNumbersDTO>>
    {
        private readonly ISurveysService _surveysService;

        public GetNumbersForSurveyRequestHandler(ISurveysService surveysService)
        {
            _surveysService = surveysService;
        }

        public async Task<Response<SurveyNumbersDTO>> Handle(
            GetNumbersForSurveyRequest request, CancellationToken cancellationToken)
            => await _surveysService.GetNumbersBySurveyIdAsync(request.Id);
    }
}
