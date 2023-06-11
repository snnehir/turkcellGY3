namespace KidegaApp.Services.CategoryService
{
    public interface ICategoryService
    {
        IEnumerable<CategoryListResponse> GetCategoryList();
    }
}
