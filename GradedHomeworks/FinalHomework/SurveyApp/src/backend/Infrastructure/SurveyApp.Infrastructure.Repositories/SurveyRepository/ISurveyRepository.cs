namespace SurveyApp.Infrastructure.Repositories.SurveyRepository
{
    public interface ISurveyRepository : IRepository<Survey>
    {
        Task<Survey?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);

        Task<IList<Survey>> GetUserSurveys(int userId);
        
    }
}
