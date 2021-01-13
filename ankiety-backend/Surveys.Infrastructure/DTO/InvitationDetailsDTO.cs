using System;

namespace Surveys.Infrastructure.DTO
{
    public class InvitationDetailsDTO
    {
        public Guid SurveyId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime SendDate { get; set; }

        public Guid UserId { get; set; }

        public int? QuestionsOnPage { get; set; }
    }
}
