namespace SurveyApp.Entities
{
    public class Notification: IEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
