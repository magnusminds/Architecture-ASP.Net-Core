using Architecture.BusinessLogic.Interface;
using Architecture.BusinessLogic.Repositories;
using Architecture.BusinessLogic.UnitOfWork;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Architecture.BusinessLogic.Extension
{
    public static class SetupBusinessLogic
    {
        public static void SetupUnitOfWorkBL(this IServiceCollection services)
        {
            services.AddTransient<IUsersBL, UsersBL>();
            services.AddTransient<IRoleBL, RoleBL>();
            services.AddTransient<IRolePermissionBL, RolePermissionBL>();
            services.AddTransient<IEmailSenderBL, EmailSenderBL>();

            //KEEP THIS LINE AT THE BOTTOM
            services.AddScoped<IUnitOfWorkBL, UnitOfWorkBL>();
        }

        public static void SetupAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}