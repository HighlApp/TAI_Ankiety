namespace Surveys.Infrastructure.DTO
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; }
        //public IEnumerable<OptionDTO> Options { get; set; }
    }
}
