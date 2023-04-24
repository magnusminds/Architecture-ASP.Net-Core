using Architecture.Core.Localizer.JsonString;

namespace Architecture.WebAPI.Middleware
{
    public static class UseJsonStringLocalizer
    {
        public static void UseLocalizationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<LocalizationMiddleware>();
        }
    }
}
