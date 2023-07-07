using SurveyApp.WebUI.Models.Requests;
using SurveyApp.WebUI.Models.Responses;

namespace SurveyApp.WebUI.Services.User
{
    public interface IUserService
    {
        Task<BaseResponseModel<LoginResponseModel>> LoginAsync(LoginRequestModel loginModel);
        Task<BaseResponseModel<SignUpResponseModel>> SignUpAsync(SignUpRequestModel registerModel);
        //Task<bool> SignUpAsync(SignUpRequestModel registerModel);
    }
}
