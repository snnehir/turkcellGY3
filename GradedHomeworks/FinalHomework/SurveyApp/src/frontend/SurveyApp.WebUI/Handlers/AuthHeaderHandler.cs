using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Net;
using SurveyApp.WebUI.Services.RefreshToken;
using SurveyApp.WebUI.Constants;

namespace SurveyApp.WebUI.Handlers
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthHeaderHandler(IHttpContextAccessor httpContextAccessor, IRefreshTokenService refreshTokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _refreshTokenService = refreshTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = await _httpContextAccessor.HttpContext.GetTokenAsync(AuthenticationParameters.AccessToken);
            HttpResponseMessage httpResponseMessage;

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                // send request with authorization header
                httpResponseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                // refresh token here if it has expired
                if (httpResponseMessage.StatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                    var tokenResponseModel = await _refreshTokenService.GetAccessTokenByRefreshTokenAsync();
                    if (tokenResponseModel is not null && tokenResponseModel.Succeeded)
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponseModel.Data.AccessToken);
                        // send request with new access token
                        httpResponseMessage = await base.SendAsync(request, cancellationToken);
                    }
                }
            }
            // send request without authorization header
            else
            {
                httpResponseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            return httpResponseMessage;
        }
    }
}
