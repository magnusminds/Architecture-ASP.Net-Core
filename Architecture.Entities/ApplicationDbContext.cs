using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Architecture.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Core Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Core Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(x => x.UserId).UseIdentityColumn();
                b.Property(x => x.UserId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });
        }

        //public DbSet<ApplicationUser> ApplicationUser { get; set; }
        //public DbSet<ApplicationRole> ApplicationRole { get; set; }
    }
}