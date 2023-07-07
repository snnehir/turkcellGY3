using Mapster;
using SurveyApp.Entities;
using SurveyApp.Infrastructure.Repositories.ResponseRepository;
using SurveyApp.Infrastructure.Repositories.SurveyRepository;
using SurveyApp.Services.Services.ResponseService;
using SurveyApp.Services.Services.SurveyService;

namespace SurveyApp.WebAPI.Extensions
{
    public static class ServiceColllectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<ISurveyRepository, EFSurveyRepository>();
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<IResponseRepository, EFResponseRepository>();
            return services;
        }
        public static IServiceCollection AddAndConfigureSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return services;
        }
        public static IServiceCollection AddAndConfigureAuthentication(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.Audience = configuration.GetSection("Token:Audience").Value;
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = configuration.GetSection("Token:Audience").Value,
                    ValidIssuer = configuration.GetSection("Token:Issuer").Value,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration.GetSection("Token:SecurityKey").Value)),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
        public static IServiceCollection AddCorsOrigins(this IServiceCollection services)
        {
            // Enable CORS: https://docs.microsoft.com/en-gb/aspnet/core/security/cors?view=aspnetcore-6.0
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                    builder =>
                    {
                        // allow web-client origin
                        builder.WithOrigins(configuration.GetSection("AllowedOrigins").Value).AllowAnyMethod().AllowAnyHeader();
                    });
            });
            return services;
        }

        public static IServiceCollection AddRedisCache(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("redis");
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
                options.InstanceName = "SampleInstance";
            });
            return services;
        }
        public static IServiceCollection AddDbContextService(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("db");

            services.AddDbContext<SurveyAppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SurveyApp.WebAPI"));
            });
            return services;
        }
        public static IServiceCollection AddSessionService(this IServiceCollection services)
        {
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(15);
            });
            return services;
        }
        public static void MapsterConfigurations(this IServiceCollection services)
        {
            TypeAdapterConfig<QuestionDto, Question>
                .NewConfig().Map(dest => dest.QuestionOptions, src => src.QuestionOptions);
            TypeAdapterConfig<CreateSurveyRequest, Survey>
                .NewConfig().Map(dest => dest.Questions, src => src.Questions);
            TypeAdapterConfig<Survey, SurveyCollectionResponse>
                .NewConfig().Map(dest => dest.ResponseCount, src => src.Responses.Count);
            TypeAdapterConfig<Survey, SurveyDisplayResponse>
                .NewConfig().Map(dest => dest.SurveyOwnerFullName, src => $"{src.SurveyOwner.FirstName} {src.SurveyOwner.LastName}");
            TypeAdapterConfig<AnswerDto, Answer>
               .NewConfig().Map(dest => dest.QuestionId, src => src.Id);
            TypeAdapterConfig<AnswerOptionDto, AnswerOption>
               .NewConfig().Map(dest => dest.QuestionOptionId, src => src.Id);
            TypeAdapterConfig<SurveyResponseModel, Response>
               .NewConfig().Map(dest => dest.Answers, src => src.Answers);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
