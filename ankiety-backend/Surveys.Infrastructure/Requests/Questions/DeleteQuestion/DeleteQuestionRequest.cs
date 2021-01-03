using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System;

namespace Surveys.Infrastructure.Requests.Questions.DeleteQuestion
{
    public class DeleteQuestionRequest : IRequest<Response<QuestionDTO>>
    {
        public Guid Id { get; set; }
    }
}
