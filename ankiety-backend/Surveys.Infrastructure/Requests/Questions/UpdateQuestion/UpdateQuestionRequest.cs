using MediatR;
using Surveys.Core.Entities;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Requests.Questions.UpdateQuestion
{
    public class UpdateQuestionRequest : IRequest<Response<QuestionDTO>>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
