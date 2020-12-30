using System;

namespace Surveys.Core.Entities
{
    public class AnswerOption
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public double? Value { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
