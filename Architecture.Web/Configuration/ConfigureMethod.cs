using Architechture.Web.Middleware;

namespace Architechture.Web.Configuration
{
    public static class ConfigureMethod
    {
        public static void ConfigureWebApplication(this WebApplication app)
        {
            // Set Common DateTime Format Globally
            app.ConfigureCulture();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseLocalizationMiddleware();
            app.UseExceptionHandlerMiddleware();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                CheckConsentNeeded = context => true,
                MinimumSameSitePolicy = SameSiteMode.None
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "MyArea",
                  template: "{area:exists}/{controller=Users}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}
