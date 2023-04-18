using Architechture.Web.Middleware;


namespace Architechture.Web.Configuration
{
    public static class MvcConfig
    {
        public static void SetupControllers(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<CurrentUserActionFilter>();
            services.AddSingleton<LocalizationMiddleware>();
            services.AddMvc(options =>
            {
                options.Filters.AddService<CurrentUserActionFilter>();
            });

        }
    }
}
