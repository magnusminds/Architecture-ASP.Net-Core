using Architecture.DataAccess.Generic;
using Architecture.DataAccess.Interface;
using Architecture.DataAccess.Repositories;
using Architecture.DataAccess.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.DataAccess.Extention
{
    public static class SetupUnitOfWork
    {
        public static void SetupUnitOfWorkDA(this IServiceCollection services)
        {
            services.AddScoped(typeof(ISqlRepository<>), typeof(SqlDBRepository<>));
            services.AddScoped<IUserRolesDA, UserRolesDA>();
            services.AddScoped<IUserDA, UserDA>();
            services.AddScoped<IRolePermissionDA, RolePermissionDA>();
            services.AddScoped<IRoleDA, RoleDA>();
            services.AddScoped<ILoginTokenDA, LoginTokenDA>();

            // KEEP THIS LINE AT THE END.
            services.AddScoped<IUnitOfWorkDA, UnitOfWorkDA>();
        }
    }
}