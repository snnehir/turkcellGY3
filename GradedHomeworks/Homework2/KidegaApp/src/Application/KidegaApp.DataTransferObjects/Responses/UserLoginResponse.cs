namespace KidegaApp.DataTransferObjects.Responses
{
    public class UserLoginResponse
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsSuccess { get; set; }
    }
}
