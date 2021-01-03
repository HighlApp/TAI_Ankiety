using MediatR;
using Surveys.Core.Entities;
using Surveys.Core.Enums;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Requests.Questions.PostQuestion
{
    public class PostQuestionRequest : IRequest<Response<QuestionDTO>>
    {
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public Guid SurveyId { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
