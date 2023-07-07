namespace SurveyApp.Infrastructure.Repositories.UserRepository
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<IList<Survey>> GetUserSurveys(int userId);
        Task<IList<Notification>> GetUserNotifications(int userId);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
