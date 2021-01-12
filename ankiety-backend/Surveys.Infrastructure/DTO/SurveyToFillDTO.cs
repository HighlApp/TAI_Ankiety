using System;
using System.Collections.Generic;

namespace Surveys.Infrastructure.DTO
{
    public class SurveyToFillDTO
    {
        public Guid Id { get; set; }

        public IEnumerable<QuestionDTO> Questions { get; set; }

        public int QuestionsOnPage { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
