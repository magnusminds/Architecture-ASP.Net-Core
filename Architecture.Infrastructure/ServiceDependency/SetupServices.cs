﻿using Architecture.BusinessLogic.Extension;
using Architecture.Core.Localizer.JsonString;
using Architecture.Core.Services.Email;
using Architecture.DataAccess.Extention;
using Architecture.Dto;
using Architecture.Infrastructure.Identity.Authorize;
using Architecture.Infrastructure.Identity.Models;
using Architecture.Infrastructure.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Architecture.Core.Constants.ApplicationIdentityConstants;

namespace Architecture.Infrastructure.ServiceDependency
{
    public static class SetupServices
    {
        public static void SetupDIServices(this IServiceCollection services, IConfiguration configuration, string AuthenticationScheme = "Cookies")
        {
            services.Configure<TokenConfiguration>(configuration.GetSection("token"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<CurrentUser>();
            services.SetAuthorization(configuration, AuthenticationScheme);
            services.SetupUnitOfWorkDA();
            services.SetupUnitOfWorkBL();
            services.SetupAutoMapper();
            services.SetupJsonStrinLocalizer();
        }

        public static void SetAuthorization(this IServiceCollection services, IConfiguration configuration, string AuthenticationScheme)
        {
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            TokenConfiguration tokenConfiguration = new TokenConfiguration();
            (configuration.GetSection("token")).Bind(tokenConfiguration);
            byte[] secret = Encoding.ASCII.GetBytes(tokenConfiguration.Secret);

            if (AuthenticationScheme == "Cookies")
            {
                services.AddAuthentication()
                    .AddCookie(options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                        options.SlidingExpiration = true;
                        options.AccessDeniedPath = "/Forbidden/";
                        options.LoginPath = "/Authorize/Login";
                    }).AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(secret),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
            }
            if (AuthenticationScheme == "Bearer")
            {
                ProjectSettings settings = new ProjectSettings()
                {
                    IsAPI = true
                };
                services.AddSingleton<ProjectSettings>(settings);

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                            .AddJwtBearer(x =>
                            {
                                x.RequireHttpsMetadata = false;
                                x.SaveToken = true;
                                x.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };
                            });
            }

            services.AddAuthorization(
                options =>
                {
                    foreach (var item in Permissions.GetAllPermissions())
                    {
                        options.AddPolicy(item, builder => { builder.AddRequirements(new PermissionRequirement(item)); });
                    }
                    options.AddPolicy(
                        JwtBearerDefaults.AuthenticationScheme,
                                           new AuthorizationPolicyBuilder()
                                               .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                               .RequireAuthenticatedUser()
                                               .Build());
                });
        }
    }
}
