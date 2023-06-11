namespace KidegaApp.Infrastructure.Repositories.AuthorRepository
{
    public class EFAuthorRepository : IAuthorRepository
    {
        private readonly KidegaDbContext _dbContext;
        public EFAuthorRepository(KidegaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task CreateAsync(Author entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Author>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Author>> GetAllWithPredicateAsync(Expression<Func<Author, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _dbContext.Authors.Include(a => a.Books)
                                                 .ThenInclude(b => b.Category)
                                                 .SingleOrDefaultAsync(a => a.Id == id);  
            
        }

        public Task UpdateAsync(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
