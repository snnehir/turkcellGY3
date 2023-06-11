namespace KidegaApp.Infrastructure.Repositories.CategoryRepository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        IList<Category> GetAll();
    }
}
