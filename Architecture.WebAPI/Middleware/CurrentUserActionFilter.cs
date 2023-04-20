using Architecture.Core.Constants;
using Architecture.Dto;
using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Architecture.WebAPI.Middleware
{
    public class CurrentUserActionFilter : IAsyncActionFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly CurrentUser _currentUser;

        public CurrentUserActionFilter(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, CurrentUser currentUser)
        {
            _currentUser = currentUser;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await CurrentUserInfo(context);
            await next();
        }

        private async Task CurrentUserInfo(ActionExecutingContext context)
        {
            if (context.HttpContext.User != null)
            {
                if (context.HttpContext.User.Identity != null)
                {
                    var user = await _userManager.GetUserAsync(context.HttpContext.User);
                    if (user != null)
                    {
                        var userRoleNames = await _userManager.GetRolesAsync(user);
                        var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name)).FirstOrDefault();
                        _currentUser.UserId = Convert.ToInt32(user.Id);
                        _currentUser.Name = user.FirstName;
                        _currentUser.FullName = user.FullName;
                        _currentUser.EmailAddress = user.Email;
                        _currentUser.RoleId = 0;
                        _currentUser.Role = string.Empty;

                        //if (context.HttpContext.Request.Cookies[ApplicationIdentityConstants.TenantCookieName] != null)
                        //{
                        //    _currentUser.TenantId = Convert.ToInt32(MagnusMinds.Utility.Encryption.Decrypt(context.HttpContext.Request.Cookies[ApplicationIdentityConstants.TenantCookieName], true, ApplicationIdentityConstants.EncryptionSecret));
                        //    var userTenantData = await _unitOfWorkBL.UserTenantMappingBL.GetAll(new CancellationToken());
                        //    if (userTenantData != null)
                        //    {
                        //        _currentUser.TenantName = userTenantData?.FirstOrDefault(x => x.TenantId == _currentUser.TenantId)?.TenantName;
                        //        _currentUser.IsMultipleTenant = userTenantData.Count() == 1 ? false : true;
                        //    }
                        //}

                        if (!string.IsNullOrEmpty(context.HttpContext.Request.Headers[ApplicationIdentityConstants.TenantHeaderName]))
                        {
                            _currentUser.TenantId = Convert.ToInt32(MagnusMinds.Utility.Encryption.Decrypt(context.HttpContext.Request.Headers[ApplicationIdentityConstants.TenantCookieName], true, ApplicationIdentityConstants.EncryptionSecret));
                        }
                    }
                }
            }
        }
    }
}
