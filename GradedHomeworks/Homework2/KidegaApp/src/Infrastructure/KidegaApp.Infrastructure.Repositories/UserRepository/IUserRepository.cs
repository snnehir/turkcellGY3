namespace KidegaApp.Infrastructure.Repositories.UserRepository
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
