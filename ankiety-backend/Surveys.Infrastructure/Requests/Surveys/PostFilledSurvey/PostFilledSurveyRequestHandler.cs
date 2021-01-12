using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.PostFilledSurvey
{
    public class PostFilledSurveyRequestHandler :
        IRequestHandler<PostFilledSurveyRequest, Response<StatusResponseDTO>>
    {
        public Task<Response<StatusResponseDTO>> Handle(PostFilledSurveyRequest request, 
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
