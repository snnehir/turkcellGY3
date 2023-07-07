namespace SurveyApp.Entities
{
    public class Question: IEntity
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsMandatory { get; set; }
        public Guid SurveyId { get; set; }
        public Survey Survey { get; set; }
        public int QuestionTypeId { get; set; }
        public QuestionType QuestionType { get; set; }
        public ICollection<QuestionOption> QuestionOptions { get; set;} = new HashSet<QuestionOption>();
        public ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();

    }
}
