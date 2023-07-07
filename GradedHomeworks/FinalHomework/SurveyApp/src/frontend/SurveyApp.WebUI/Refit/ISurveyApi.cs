using Microsoft.AspNetCore.Mvc;
using Refit;
using SurveyApp.WebUI.Models.Requests;
using SurveyApp.WebUI.Models.Responses;

namespace SurveyApp.WebUI.Refit
{
    public interface ISurveyApi
    {
        [Post("/Account/register")]
        Task<IApiResponse<SignUpResponseModel>> RegisterAsync(SignUpRequestModel registerModel);

        [Post("/Account/login")]
        Task<IApiResponse<LoginResponseModel>> LoginAsync(LoginRequestModel loginModel);

        [Post("/Survey/{surveyId}")]
        Task<IApiResponse<SurveyResponseModel>> GetSurveyByIdAsync(Guid surveyId);

        [Post("/Survey/SendResponse")]
        Task SendSurveyResponse(SendSurveyResponseRequestModel request);
    }
}
