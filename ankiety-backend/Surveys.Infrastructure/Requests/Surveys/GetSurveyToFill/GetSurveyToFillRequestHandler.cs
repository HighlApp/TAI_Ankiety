using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.GetSurveyToFill
{
    public class GetSurveyToFillRequestHandler :
        IRequestHandler<GetSurveyToFillRequest, Response<SurveyToFillDTO>>
    {
        public Task<Response<SurveyToFillDTO>> Handle(
            GetSurveyToFillRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();

            //TO DO
        }
    }
}
