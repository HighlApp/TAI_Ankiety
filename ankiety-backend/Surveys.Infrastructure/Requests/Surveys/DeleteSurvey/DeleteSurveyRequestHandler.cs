using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Requests.Surveys.DeleteSurvey
{
    class DeleteSurveyRequestHandler : IRequestHandler<DeleteSurveyRequest, Response<SurveyDTO>>
    {
        private readonly ISurveysService _surveysService;
        public DeleteSurveyRequestHandler(ISurveysService surveysService)
        {
            _surveysService = surveysService;
        }

        public async Task<Response<SurveyDTO>> Handle(DeleteSurveyRequest request, CancellationToken cancellationToken)
            => await _surveysService.DeleteAsync(request.Id);
    }
}
