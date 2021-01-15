using System.Collections.Generic;

namespace Surveys.Infrastructure.DTO
{
    public class FilledSurveySummaryDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double? MaxValue { get; set; }

        public double? Value { get; set; }

        public UserDTO User { get; set; }

        public List<FilledSurveyQuestionDTO> Questions { get; set; }

        public string Type { get; set; }
    }
}
