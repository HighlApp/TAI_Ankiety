using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Surveys.GetFilledSurvey
{
    public class GetFilledSurveyRequestHandler :
        IRequestHandler<GetFilledSurveyRequest, Response<FilledSurveySummaryDTO>>
    {
        private readonly ISurveysService _surveysService;

        public GetFilledSurveyRequestHandler(ISurveysService surveysService)
        {
            _surveysService = surveysService;
        }

        public async Task<Response<FilledSurveySummaryDTO>> Handle(
            GetFilledSurveyRequest request, CancellationToken cancellationToken)
            => await _surveysService.GetFilledSurvey(request.InvitationId);
    }
}
