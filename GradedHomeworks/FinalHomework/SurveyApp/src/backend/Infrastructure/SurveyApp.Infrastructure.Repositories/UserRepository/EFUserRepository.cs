namespace SurveyApp.Infrastructure.Repositories.UserRepository
{
    public class EFUserRepository : IUserRepository
    {
        private readonly SurveyAppDbContext _dbContext;
        public EFUserRepository(SurveyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<User>> GetAllAsync() => await _dbContext.Users.AsNoTracking().ToListAsync();

        public async Task<User?> GetByIdAsync(int id) => await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

        public async Task<IList<Notification>> GetUserNotifications(int userId)
        {
            return await _dbContext.Notifications.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<IList<Survey>> GetUserSurveys(int userId)
        {
            return await _dbContext.Surveys.Where(n => n.SurveyOwnerId == userId).ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task UpdateAsync(User entity)
        {
            _dbContext.Users.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
