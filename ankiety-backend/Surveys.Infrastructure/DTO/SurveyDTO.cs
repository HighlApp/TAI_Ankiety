using System;
using Surveys.Core.Enums;
using System.Collections.Generic;

namespace Surveys.Infrastructure.DTO
{
    public class SurveyDTO
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SurveyType SurveyType { get; set; }

        public int? QuestionsOnPage { get; set; }

        public IEnumerable<QuestionDTO> Questions { get; set; }
    }
}
