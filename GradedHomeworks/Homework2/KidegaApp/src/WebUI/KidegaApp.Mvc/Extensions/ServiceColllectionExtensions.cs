namespace KidegaApp.Mvc.Extensions
{
    public static class ServiceColllectionExtensions
    {
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
    }
}
