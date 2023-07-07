namespace SurveyApp.Entities
{
    public class QuestionOption: IEntity
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public ICollection<AnswerOption> AnswerOptions { get; set; } = new HashSet<AnswerOption>();
    }
}
