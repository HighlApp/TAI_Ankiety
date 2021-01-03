using System;

namespace Surveys.Core.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? OptionId { get; set; }
        public string AnswerText { get; set; }
        public virtual Option Option { get; set; }
    }
}
