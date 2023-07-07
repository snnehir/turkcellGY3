using SurveyApp.WebUI.Models.Requests;
using SurveyApp.WebUI.Models.Responses;

namespace SurveyApp.WebUI.Services.Survey
{
    public interface ISurveyService
    {
        Task<BaseResponseModel<SurveyViewModel>> GetSurveyById(Guid id);
        Task SendSurveyResponse(SendSurveyResponseRequestModel request);
    }
}
