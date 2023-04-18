using Architecture.BusinessLogic.UnitOfWork;
using Architecture.Core.Constants;
using Architecture.Dto;
using Architecture.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Architechture.Web.Middleware
{
    public class CurrentUserActionFilter : IAsyncActionFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly CurrentUser _currentUser;
        private readonly IUnitOfWorkBL _unitOfWorkBL;
        private readonly IConfiguration _configuration;

        public CurrentUserActionFilter(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, CurrentUser currentUser, IUnitOfWorkBL unitOfWorkBL, IConfiguration configuration)
        {
            _currentUser = currentUser;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWorkBL = unitOfWorkBL;
            _configuration = configuration;
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
                    var path = _configuration["AppSettings:HostURL"];
                    var user = await _userManager.GetUserAsync(context.HttpContext.User);
                    if (user != null)
                    {
                        if (user.IsActive == true && user.IsDeleted == false)
                        {
                            var userRoleNames = await _userManager.GetRolesAsync(user);
                            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name)).FirstOrDefault();
                            _currentUser.UserId = user.Id;
                            _currentUser.Name = user.FirstName;
                            _currentUser.FullName = user.FullName;
                            _currentUser.EmailAddress = user.Email;
                            _currentUser.RoleId = userRoles?.Id;
                            _currentUser.Role = userRoles?.Name;
                            if (context.HttpContext.Request.Cookies[ApplicationIdentityConstants.TenantCookieName] != null)
                            {
                                _currentUser.TenantId = Convert.ToInt32(MagnusMinds.Utility.Encryption.Decrypt(context.HttpContext.Request.Cookies[ApplicationIdentityConstants.TenantCookieName], true, ApplicationIdentityConstants.EncryptionSecret));
                                //var userTenantData = await _unitOfWorkBL.UserTenantMappingBL.GetAll(new CancellationToken());
                                //if (userTenantData != null)
                                //{
                                //    _currentUser.TenantName = userTenantData?.FirstOrDefault(x => x.TenantId == _currentUser.TenantId)?.TenantName;
                                //    _currentUser.TenantLogo = userTenantData?.FirstOrDefault(x => x.TenantId == _currentUser.TenantId)?.TenantLogo == null ? path + "/images/no-coverimage.png" : userTenantData?.FirstOrDefault(x => x.TenantId == _currentUser.TenantId)?.TenantLogo;
                                //    _currentUser.IsMultipleTenant = userTenantData.Count() == 1 ? false : true;
                                //}
                            }
                            _currentUser.Role = userRoles?.Name.Replace("_" + Convert.ToString(_currentUser.TenantId), "");
                        }
                    }
                }
            }
        }
    }
}
