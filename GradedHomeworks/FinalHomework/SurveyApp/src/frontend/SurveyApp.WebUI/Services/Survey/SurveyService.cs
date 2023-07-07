using SurveyApp.WebUI.Models.Requests;
using SurveyApp.WebUI.Models.Responses;
using SurveyApp.WebUI.Refit;
using System.Text.Json;

namespace SurveyApp.WebUI.Services.Survey
{
    public class SurveyService: ISurveyService
    {
        private readonly ISurveyApi _surveyApi;
        public SurveyService(ISurveyApi surveyApi)
        {
            _surveyApi = surveyApi;
        }
        public async Task<BaseResponseModel<SurveyViewModel>> GetSurveyById(Guid id)
        {
            var response = await _surveyApi.GetSurveyByIdAsync(id);
            if (!response.IsSuccessStatusCode)
            {
                var responseFail = JsonSerializer.Deserialize<BaseResponseModel<SurveyViewModel>>(response.Error.Content);
                return responseFail;

            }
            var data = new SurveyViewModel()
            {
                Survey = response.Content
            };
            var responseSuccess = new BaseResponseModel<SurveyViewModel>() { Succeeded = true, Data = data };
            return responseSuccess;
        }

        public async Task SendSurveyResponse(SendSurveyResponseRequestModel request)
        {
            await _surveyApi.SendSurveyResponse(request);
        }
    }
}
