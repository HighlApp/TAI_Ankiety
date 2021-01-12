using System;

namespace Surveys.Infrastructure.DTO
{
    public class AdministratorInvitationDTO
    {
        public int? Id { get; set; }

        public string UserId { get; set; }

        public Guid SurveyId { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public bool Expired { get; set; }

        public string Username { get; set; }

        public string SurveyName { get; set; }

        public bool FilledSurvey { get; set; }

        public int? QuestionsOnPage { get; set; }
    }
}
