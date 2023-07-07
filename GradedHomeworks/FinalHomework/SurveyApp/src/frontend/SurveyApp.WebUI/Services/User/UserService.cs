using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using SurveyApp.WebUI.Constants;
using SurveyApp.WebUI.Models.Requests;
using System.Security.Claims;
using SurveyApp.WebUI.Refit;
using SurveyApp.WebUI.Models.Responses;
using System.Text.Json;
using NuGet.Protocol.Plugins;

namespace SurveyApp.WebUI.Services.User
{
    public class UserService : IUserService
    {
        private readonly ISurveyApi _surveyApi;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(ISurveyApi surveyApi, IHttpContextAccessor httpContextAccessor)
        {
            _surveyApi = surveyApi;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<BaseResponseModel<SignUpResponseModel>> SignUpAsync(SignUpRequestModel registerModel)
        {
            var registerResponse = await _surveyApi.RegisterAsync(registerModel);

            if (!registerResponse.IsSuccessStatusCode)
            {
                var responseFail = JsonSerializer.Deserialize<BaseResponseModel<SignUpResponseModel>>(registerResponse.Error.Content);
                return responseFail;

            }
            var responseSuccess = new BaseResponseModel<SignUpResponseModel>() { Succeeded = true};
            return responseSuccess;

        }

        public async Task<BaseResponseModel<LoginResponseModel>> LoginAsync(LoginRequestModel loginModel)
        {
            var loginResponse = await _surveyApi.LoginAsync(loginModel);

            if (!loginResponse.IsSuccessStatusCode)
            {
                var responseFail = JsonSerializer.Deserialize<BaseResponseModel<LoginResponseModel>>(loginResponse.Error.Content);
                return responseFail;

            }

            var loginResponseData = loginResponse.Content;
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, loginResponseData.Email),
                new Claim(ClaimTypes.Role, loginResponseData.Role),
            };

            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

            AuthenticationProperties authenticationProperties = new();
            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
                {
                    new AuthenticationToken
                    {
                        Name =  AuthenticationParameters.AccessToken,
                        Value = loginResponseData.AccessToken
                    },
                    new AuthenticationToken
                    {
                        Name = AuthenticationParameters.RefreshToken,
                        Value = loginResponseData.RefreshToken.TokenString
                    },
                    new AuthenticationToken
                    {
                        Name = AuthenticationParameters.ExpiresIn,
                        Value = loginResponseData.RefreshToken.ExpireAt.ToString()
                    }
                });

            authenticationProperties.IsPersistent = true;
            authenticationProperties.ExpiresUtc = loginResponseData.RefreshToken.ExpireAt;
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            var response = new BaseResponseModel<LoginResponseModel>
            {
                Succeeded = true,
                Data = loginResponseData
            };
            return response;

        }
    }
}
