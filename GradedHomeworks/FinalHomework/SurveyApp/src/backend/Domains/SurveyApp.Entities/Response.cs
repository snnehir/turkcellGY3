namespace SurveyApp.Entities
{
    public class Response: IEntity
    {
        public int Id { get; set; }
        public Guid SurveyId { get; set; }
        public Survey Survey { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
