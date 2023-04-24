using Architechture.Web.Controllers;
using Architecture.BusinessLogic.Authentication;
using Architecture.BusinessLogic.Interface;
using Architecture.Entities;
using Architecture.Web.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Architecture.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionFilter(permission: "Admin", customClaimTypes: Architecture.BusinessLogic.CustomClaimTypes.RoleName)]
    public class UsersController : BaseController
    {
        private IUsersBL _usersBL;

        public UsersController(IUsersBL usersBL, IHostEnvironment hostEnvironment, IModelMetadataProvider modelMetadataProvider, SiteConfiguration siteConfiguration) : base(hostEnvironment, modelMetadataProvider, siteConfiguration)
        {
            _usersBL = usersBL;
        }

        //[Authorize(ApplicationIdentityConstants.Permissions.Users.View)]
        public IActionResult Index()
        {
           // var data = _usersBL.GetUsersEntity();
            return View();
        }
        [HttpGet]
        //[Authorize(ApplicationIdentityConstants.Permissions.Users.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        // [Authorize(ApplicationIdentityConstants.Permissions.Users.Create)]
        public IActionResult CreatePost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var newUser = _usersBL.CreataUser(users);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return View();
        }

        [HttpGet]
        // [Authorize(ApplicationIdentityConstants.Permissions.Users.View)]
        public IActionResult Edit(long Id)
        {
           // var oldUser = _usersBL.GetUsersEntityById(Id);
            return View();
        }


        [HttpPost]
        // [Authorize(ApplicationIdentityConstants.Permissions.Users.Update)]
        public IActionResult Edit()
        {
            try
            {
                if (ModelState.IsValid)
                {
                  //  var newUser = _usersBL.UpdateUser(users);
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {

                //throw;
            }
            return View();
        }
    }
}