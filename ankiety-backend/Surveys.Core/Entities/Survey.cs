using System;
using Surveys.Core.Enums;
using System.Collections.Generic;

namespace Surveys.Core.Entities
{
    public class Survey
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SurveyType SurveyType { get; set; }

        public DateTime Created { get; set; }

        public int? QuestionsOnPage { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
