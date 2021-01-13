using System;

namespace Surveys.Infrastructure.DTO
{
    public class UserInvitationDTO
    {
        public Guid Id { get; set; }

        public Guid SurveyId { get; set; }

        public string Name { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
