using Architecture.WebAPI.Middleware;

namespace Architecture.WebAPI.Configuration
{
    public static class MvcConfig
    {
        public static void  SetupControllers(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<CurrentUserActionFilter>();
            services.AddMvc(options =>
            {
                options.Filters.AddService<CurrentUserActionFilter>();
            });

        }
    }
    
}
