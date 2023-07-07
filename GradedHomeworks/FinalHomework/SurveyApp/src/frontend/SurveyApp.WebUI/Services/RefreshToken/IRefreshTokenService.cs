using SurveyApp.WebUI.Models.Responses;

namespace SurveyApp.WebUI.Services.RefreshToken
{
    public interface IRefreshTokenService
    {
        Task<BaseResponseModel<RefreshTokenResponseModel>> GetAccessTokenByRefreshTokenAsync();
    }
}
