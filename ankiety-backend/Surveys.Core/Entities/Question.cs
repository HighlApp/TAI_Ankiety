using Surveys.Core.Enums;
using System;
using System.Collections.Generic;

namespace Surveys.Core.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public Guid SurveyId { get; set; }
        public DateTime Created { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual ICollection<Option> Options { get; set; }
    }
}
