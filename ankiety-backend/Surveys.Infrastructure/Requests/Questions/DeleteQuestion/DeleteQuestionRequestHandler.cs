using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Requests.Questions.DeleteQuestion
{
    public class DeleteQuestionRequestHandler : IRequestHandler<DeleteQuestionRequest, Response<QuestionDTO>>
    {
        private readonly IQuestionsService _questionsService;
        public DeleteQuestionRequestHandler(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        public async Task<Response<QuestionDTO>> Handle(DeleteQuestionRequest request, CancellationToken cancellationToken)
            => await _questionsService.DeleteAsync(request.Id);
    }
}
