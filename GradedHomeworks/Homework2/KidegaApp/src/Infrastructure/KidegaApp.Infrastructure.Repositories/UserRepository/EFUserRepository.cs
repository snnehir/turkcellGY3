namespace KidegaApp.Infrastructure.Repositories.UserRepository
{
    public class EFUserRepository : IUserRepository
    {
        private readonly KidegaDbContext dbContext;
        public EFUserRepository(KidegaDbContext context)
        {
            this.dbContext = context;
        }
        public async Task CreateAsync(User entity)
        {
            await dbContext.Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await dbContext.Users.FindAsync(id);
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
        public async Task<IList<User?>> GetAllAsync()
        {
            return await dbContext.Users.AsNoTracking().ToListAsync();
        }

        public Task<IList<User>> GetAllWithPredicateAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await dbContext.Users.SingleOrDefaultAsync(u => u.Email.Equals(email));
        }


        public async Task UpdateAsync(User entity)
        {
            dbContext.Users.Update(entity);
            await dbContext.SaveChangesAsync();

        }
    }
}
