using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Questions.GetQuestion
{
    class GetQuestionRequestHandler : IRequestHandler<GetQuestionRequest, Response<QuestionDTO>>
    {
        private readonly IQuestionsService _questionsService;

        public GetQuestionRequestHandler(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        public async Task<Response<QuestionDTO>> Handle(
            GetQuestionRequest request, CancellationToken cancellationToken)
            => await _questionsService.GetByIdAsync(request.Id);
    }
}
