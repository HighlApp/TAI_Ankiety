using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System;

namespace Surveys.Infrastructure.Requests.Questions.GetQuestion
{
    public class GetQuestionRequest : IRequest<Response<QuestionDTO>>
    {
        public Guid Id { get; set; }
    }
}
