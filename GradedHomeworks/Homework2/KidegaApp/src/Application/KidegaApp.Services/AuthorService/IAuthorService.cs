namespace KidegaApp.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<AuthorDisplayResponse> GetByIdAsync(int id);
    }
}
