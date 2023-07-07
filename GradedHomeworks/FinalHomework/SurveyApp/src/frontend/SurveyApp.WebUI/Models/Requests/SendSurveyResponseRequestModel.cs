namespace SurveyApp.WebUI.Models.Requests
{
    public class SendSurveyResponseRequestModel
    {
        public string SurveyId { get; set; }
        public IEnumerable<AnswerDto> Answers { get; set; }

    }

    public class AnswerDto
    {
        public string? Text { get; set; }
        public byte? Rate { get; set; }
        public int Id { get; set; }
        public ICollection<AnswerOptionDto> AnswerOptions { get; set; } //= new HashSet<AnswerOptionDto>();
    }

    public class AnswerOptionDto
    {
        public int Id { get; set; }
    }
}
