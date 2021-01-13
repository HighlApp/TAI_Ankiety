using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Questions.UpdateQuestion
{
    public class UpdateQuestionRequestHandler : IRequestHandler<UpdateQuestionRequest, Response<QuestionDTO>>
    {
        private readonly IQuestionsService _questionsService;

        public UpdateQuestionRequestHandler(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        public async Task<Response<QuestionDTO>> Handle(
            UpdateQuestionRequest request, CancellationToken cancellationToken)
            => await _questionsService.UpdateAsync(request);
    }
}
