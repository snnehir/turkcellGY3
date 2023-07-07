namespace SurveyApp.Entities
{
    public class Answer: IEntity
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public byte? Rate { get; set; }
        public int ResponseId { get; set; }
        public Response Response { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public ICollection<AnswerOption> AnswerOptions { get; set; } = new HashSet<AnswerOption>();

    }
}
