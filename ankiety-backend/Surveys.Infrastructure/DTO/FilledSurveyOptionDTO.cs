namespace Surveys.Infrastructure.DTO
{
    public class FilledSurveyOptionDTO
    {
        public string Text { get; set; }

        public double? Value { get; set; }

        public bool IsSelected { get; set; }
    }
}
