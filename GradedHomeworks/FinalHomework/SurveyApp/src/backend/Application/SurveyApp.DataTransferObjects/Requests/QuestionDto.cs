namespace SurveyApp.DataTransferObjects.Requests
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsMandatory { get; set; }
        public int QuestionTypeId { get; set; }
        public ICollection<QuestionOptionDto>? QuestionOptions { get; set; }
    }
}
