namespace SurveyApp.Entities
{
    public class User: IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }
        public ICollection<Survey> Surveys { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
