using System;

namespace Surveys.Infrastructure.DTO
{
    public class QuestionAnswerDTO
    {
        public int Id { get; set; }

        public Guid InvitationId { get; set; }

        public Guid QuestionId { get; set; }

        public Guid? OptionId { get; set; }

        public string AnswerText { get; set; }
    }
}
