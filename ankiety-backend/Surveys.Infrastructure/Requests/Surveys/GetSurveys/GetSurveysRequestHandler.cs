using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Requests.Surveys.GetSurveys
{
    class GetSurveysRequestHandler : IRequestHandler<GetSurveysRequest, Response<IEnumerable<SurveyDTO>>>
    {
        private readonly ISurveysService _surveysService;
        public GetSurveysRequestHandler(ISurveysService surveysService)
        {
            _surveysService = surveysService;
        }

        public async Task<Response<IEnumerable<SurveyDTO>>> Handle(GetSurveysRequest request, CancellationToken cancellationToken)
            => await _surveysService.GetAllAsync();
    }
}
