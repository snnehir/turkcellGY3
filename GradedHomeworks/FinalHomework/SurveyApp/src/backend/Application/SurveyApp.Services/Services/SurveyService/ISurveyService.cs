namespace SurveyApp.Services.Services.SurveyService
{
    public interface ISurveyService
    {
        Task<IEnumerable<SurveyCollectionResponse>> GetUserSurveys();
        Task<CreateSurveyResponse> CreateSurvey(CreateSurveyRequest request);
        Task<SurveyDisplayResponse> GetSurveyById(Guid surveyId);
    }
}
