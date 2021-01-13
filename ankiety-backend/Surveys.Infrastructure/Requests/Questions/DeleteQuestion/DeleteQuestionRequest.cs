using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Questions.DeleteQuestion
{
    public class DeleteQuestionRequest : IRequest<Response<QuestionDTO>>
    {
        public DeleteQuestionRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
