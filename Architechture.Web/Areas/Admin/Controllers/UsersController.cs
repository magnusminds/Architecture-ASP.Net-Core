using Architecture.Web.Configuration;
using Architecture.Web.Controllers;
using Architecture.BusinessLogic;
using Architecture.BusinessLogic.Authentication;
using Architecture.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Architecture.Core.Constants;
using Architecture.BusinessLogic.Repositories;
using Architechture.Web.Controllers;

namespace Architecture.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionFilter(permission: "Admin", customClaimTypes: Architecture.BusinessLogic.CustomClaimTypes.RoleName)]
    public class UsersController : BaseController
    {
        private IUsersBL _usersBL;

        public UsersController(IUsersBL usersBL, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider, SiteConfiguration siteConfiguration) : base(hostingEnvironment, modelMetadataProvider, siteConfiguration)
        {
            _usersBL = usersBL;
        }

        //[Authorize(ApplicationIdentityConstants.Permissions.Users.View)]
        public IActionResult Index()
        {
            var data = _usersBL.GetUsersEntity();
            return View(data);
        }
        [HttpGet]
        //[Authorize(ApplicationIdentityConstants.Permissions.Users.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
       // [Authorize(ApplicationIdentityConstants.Permissions.Users.Create)]
        public IActionResult Create(UsersEntity users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = _usersBL.CreataUser(users);
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {

                //throw;
            }
            return View(users);
        }

        [HttpGet]
       // [Authorize(ApplicationIdentityConstants.Permissions.Users.View)]
        public IActionResult Edit(long Id)
        {
            var oldUser = _usersBL.GetUsersEntityById(Id);
            return View(oldUser);
        }


        [HttpPost]
       // [Authorize(ApplicationIdentityConstants.Permissions.Users.Update)]
        public IActionResult Edit(UsersEntity users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = _usersBL.UpdateUser(users);
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {

                //throw;
            }
            return View(users);
        }
    }
}