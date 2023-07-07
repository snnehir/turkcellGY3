namespace SurveyApp.WebUI.Models.Responses
{
    public class LoginResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public string Role { get; set; }
    }
}
