using KidegaApp.Entities;
using KidegaApp.Infrastructure.Repositories.AuthorRepository;
using KidegaApp.Infrastructure.Repositories.BookRepository;
using KidegaApp.Infrastructure.Repositories.CategoryRepository;
using KidegaApp.Services.AuthorService;
using KidegaApp.Services.BookService;
using KidegaApp.Services.CategoryService;
using System.Reflection;

namespace KidegaApp.Mvc.Extensions
{
    public static class ServiceColllectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, EFBookRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, EFCategoryRepository>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IAuthorRepository, EFAuthorRepository>();
            return services;
        }
        public static IServiceCollection AddDbContextService(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("db");

            services.AddDbContext<KidegaDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
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

        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/User/Login";
                    opt.AccessDeniedPath = "/User/AccessDenied";
                    opt.ReturnUrlParameter = "redirectUrl";
                });
            return services;
        }

        public static void MapsterConfigurations(this IServiceCollection services)
        {

            TypeAdapterConfig<Book, BookDisplayResponse>
                .NewConfig().Map(dest => dest.Author, src => $"{src.Author.FirstName} {src.Author.LastName}")
                            .Map(dest => dest.AuthorId, src => src.AuthorId);

            TypeAdapterConfig<Book, AuthorBook>
                .NewConfig().Map(dest => dest.CategoryName, src => src.Category.Name);

            TypeAdapterConfig<Author, AuthorDisplayResponse>
                .NewConfig().Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");



            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
