using System.Collections.Generic;

namespace Surveys.Infrastructure.DTO
{
    public class FilledSurveyQuestionDTO
    {
        public string Type { get; set; }

        public string Text { get; set; }

        public string AnswerText { get; set; }

        public List<FilledSurveyOptionDTO> Options { get; set; }
    }
}
