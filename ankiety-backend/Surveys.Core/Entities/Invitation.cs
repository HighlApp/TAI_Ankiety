using System;

namespace Surveys.Core.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public Guid SurveyId { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? FilledDate { get; set; }

        public virtual User User { get; set; }

        public virtual Survey Survey { get; set; }
    }
}
