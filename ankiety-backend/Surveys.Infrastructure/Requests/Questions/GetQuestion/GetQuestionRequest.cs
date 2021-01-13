using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Questions.GetQuestion
{
    public class GetQuestionRequest : IRequest<Response<QuestionDTO>>
    {
        public GetQuestionRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
