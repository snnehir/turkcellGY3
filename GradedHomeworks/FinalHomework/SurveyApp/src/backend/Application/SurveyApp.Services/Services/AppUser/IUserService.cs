namespace SurveyApp.Services.Services.AppUser
{
    public interface IUserService
    {
        Task<bool> CheckUserExistById(int id);
        Task<User?> GetUserByEmail(string email);
        Task CreateAsync(User user);
        Task<int> GetCurrentUserId();
    }
}
