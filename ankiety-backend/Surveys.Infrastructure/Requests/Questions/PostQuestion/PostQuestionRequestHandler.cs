using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;


namespace Surveys.Infrastructure.Requests.Questions.PostQuestion
{
    public class PostQuestionRequestHandler : IRequestHandler<PostQuestionRequest, Response<QuestionDTO>>
    {
        private readonly IQuestionsService _questionsService;

        public PostQuestionRequestHandler(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        public async Task<Response<QuestionDTO>> Handle(
            PostQuestionRequest request, CancellationToken cancellationToken)
            => await _questionsService.PostAsync(request);
    }
}
