namespace KidegaApp.Infrastructure.Repositories.BookRepository
{
    public class EFBookRepository : IBookRepository
    {
        private readonly KidegaDbContext _dbContext;
        public EFBookRepository(KidegaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(Book entity)
        {
            await _dbContext.Books.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deleting = await _dbContext.Books.FindAsync(id);
            _dbContext.Books.Remove(deleting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Book>> GetAllAsync()
        {
            return await _dbContext.Books.AsNoTracking()
                                         .Include(b => b.Author)
                                         .Include(b => b.Category)
                                         .ToListAsync();

        }
        

        public async Task<IList<Book>> GetAllWithPredicateAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _dbContext.Books.AsNoTracking()
                                         .Where(predicate)
                                         .Include(b => b.Author)
                                         .Include(b => b.Category)
                                         .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _dbContext.Books.AsNoTracking()
                                         .Where(p=> p.CategoryId ==  categoryId)
                                         .Include(p => p.Author)
                                         .Include(p => p.Category)
                                         .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id) => await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);

        public Task UpdateAsync(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
