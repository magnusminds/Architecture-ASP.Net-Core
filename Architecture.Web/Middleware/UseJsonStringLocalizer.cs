namespace Architechture.Web.Middleware
{
    public static class UseJsonStringLocalizer
    {
        public static void UseLocalizationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<LocalizationMiddleware>();
        }
    }
}