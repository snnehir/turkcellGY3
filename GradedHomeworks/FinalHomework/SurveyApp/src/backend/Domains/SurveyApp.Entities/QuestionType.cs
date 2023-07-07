namespace SurveyApp.Entities
{
    public class QuestionType: IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
