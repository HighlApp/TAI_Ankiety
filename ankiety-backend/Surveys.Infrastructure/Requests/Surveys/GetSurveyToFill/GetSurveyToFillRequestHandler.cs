using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Surveys.GetSurveyToFill
{
    public class GetSurveyToFillRequestHandler :
        IRequestHandler<GetSurveyToFillRequest, Response<SurveyToFillDTO>>
    {
        private readonly ISurveysService _surveysService;

        public GetSurveyToFillRequestHandler(ISurveysService surveysService)
        {
            _surveysService = surveysService;
        }

        public async Task<Response<SurveyToFillDTO>> Handle(
            GetSurveyToFillRequest request, CancellationToken cancellationToken)
            => await _surveysService.GetSurveyToFillAsync(request.InvitationId);
    }
}
