using Architecture.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Architecture.Infrastructure.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Seed Identity data
        /// </summary>
        /// <param name="WebApplication"></param>
        /// <returns></returns>
        public static async Task SeedIdentityDataAsync(this IServiceProvider WebApplication)
        {
            using (var serviceScope = WebApplication.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                await Identity.Seed.ApplicationDbContextDataSeed.SeedAsync(userManager, roleManager);
            }
        }
    }
}
