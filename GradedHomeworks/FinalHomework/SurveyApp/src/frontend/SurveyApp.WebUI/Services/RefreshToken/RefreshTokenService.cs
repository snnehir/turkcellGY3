using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using SurveyApp.WebUI.Constants;
using SurveyApp.WebUI.Models.Responses;
using System.Net.Http.Headers;
using System.Text;
using SurveyApp.WebUI.Models.Requests;

namespace SurveyApp.WebUI.Services.RefreshToken
{
    public class RefreshTokenService : IRefreshTokenService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public RefreshTokenService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public async Task<BaseResponseModel<RefreshTokenResponseModel>> GetAccessTokenByRefreshTokenAsync()
        {
            string accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(AuthenticationParameters.AccessToken);
            string refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(AuthenticationParameters.RefreshToken);
            var requestValue = JsonConvert.SerializeObject(new RefreshTokenRequestModel() { RefreshToken = refreshToken });

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(requestValue, Encoding.UTF8, "application/json");
            var url = new Uri(_configuration.GetSection("SurveyApi:Url").Value + "/Account/refresh-token");

            var result = await client.PostAsync(url, content);
            var resultContent = await result.Content.ReadAsStringAsync();
            var tokenResponseModel = JsonConvert.DeserializeObject<BaseResponseModel<RefreshTokenResponseModel>>(resultContent);

            if (tokenResponseModel is not null && tokenResponseModel.Succeeded)
            {
                List<AuthenticationToken> authenticationTokens = new()
                {
                    new AuthenticationToken
                    {
                        Name = AuthenticationParameters.AccessToken,
                        Value = tokenResponseModel.Data.AccessToken
                    },
                    new AuthenticationToken
                    {
                        Name = AuthenticationParameters.RefreshToken,
                        Value = tokenResponseModel.Data.RefreshToken.TokenString
                    },
                    new AuthenticationToken
                    {
                        Name = AuthenticationParameters.ExpiresIn,
                        Value = tokenResponseModel.Data.RefreshToken.ExpireAt.ToString()
                    }
                };
                AuthenticateResult authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync();
                AuthenticationProperties authenticationProperties = authenticateResult.Properties;
                authenticationProperties.StoreTokens(authenticationTokens);
                authenticationProperties.ExpiresUtc = tokenResponseModel.Data.RefreshToken.ExpireAt;
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticateResult.Principal, authenticationProperties);
            }
            return tokenResponseModel;
        }
    }
}
