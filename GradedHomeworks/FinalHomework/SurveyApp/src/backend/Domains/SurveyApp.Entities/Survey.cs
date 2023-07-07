namespace SurveyApp.Entities
{
    public class Survey: IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SurveyStatus { get; set; }
        public int SurveyOwnerId { get; set; }
        public User SurveyOwner { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}
