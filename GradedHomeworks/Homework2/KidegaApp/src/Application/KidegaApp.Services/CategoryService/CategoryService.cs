namespace KidegaApp.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) { 
            _categoryRepository = categoryRepository;
        }
        public IEnumerable<CategoryListResponse> GetCategoryList()
        {
            var categories = _categoryRepository.GetAll();
            return categories.Adapt<IEnumerable<CategoryListResponse>>();
        }
    }
}
