using System;

namespace Surveys.Core.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? AnswerOptionId { get; set; }
        public string AnswerText { get; set; }
        public virtual AnswerOption AnswerOption { get; set; }
    }
}
