using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Newtonsoft.Json;
using Refit;
using SurveyApp.WebUI.Handlers;
using SurveyApp.WebUI.Refit;
using SurveyApp.WebUI.Services.RefreshToken;
using SurveyApp.WebUI.Services.Survey;
using SurveyApp.WebUI.Services.User;
using System.Globalization;
using System.Reflection;

namespace SurveyApp.WebUI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<AuthHeaderHandler>();
            return services;
        }
        public static IServiceCollection AddRefits(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddRefitClient<ISurveyApi>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                })
            }).ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(configuration.GetSection("SurveyApi:Url").Value))
            .AddHttpMessageHandler<AuthHeaderHandler>();
            return services;
        }
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/Account/Login";
                    opt.AccessDeniedPath = "/Account/AccessDenied";
                    opt.ReturnUrlParameter = "redirectUrl";
                    opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    opt.SlidingExpiration = false;
                    opt.Cookie = new()
                    {
                        Name = Assembly.GetEntryAssembly()?.GetName()?.Name?.ToLower(CultureInfo.InvariantCulture),
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict,
                        SecurePolicy = CookieSecurePolicy.Always
                    };
                });

            return services;
        }
        public static IServiceCollection ConfigureCookiePolicy(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            return services;
        }
    }
}
