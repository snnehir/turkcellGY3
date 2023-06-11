using KidegaApp.Infrastructure.Repositories.AuthorRepository;

namespace KidegaApp.Services.AuthorService
{
    public class AuthorService: IAuthorService
    {
        private readonly IAuthorRepository _authorRepository; 
        public AuthorService(IAuthorRepository authorRepository) { 
            _authorRepository = authorRepository;
        }

        public async Task<AuthorDisplayResponse> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return author.Adapt<AuthorDisplayResponse>();
        }
    }
}
