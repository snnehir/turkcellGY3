namespace KidegaApp.Infrastructure.Repositories.CategoryRepository
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly KidegaDbContext _dbContext;
        public EFCategoryRepository(KidegaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(Category entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deleting = await _dbContext.Categories.FindAsync(id);
            _dbContext.Categories.Remove(deleting);
            await _dbContext.SaveChangesAsync();
        }

        public IList<Category> GetAll() => _dbContext.Categories.AsNoTracking().ToList();

        public async Task<IList<Category>> GetAllAsync() => await _dbContext.Categories.AsNoTracking().ToListAsync();

        public Task<IList<Category>> GetAllWithPredicateAsync(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Category?> GetByIdAsync(int id) => await _dbContext.Categories.SingleOrDefaultAsync(c=>c.Id == id);

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
