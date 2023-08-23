using Architechture.Web.Controllers;
using Architecture.BusinessLogic.Interface;
using Architecture.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Architecture.Web.Controllers
{
    public class LoginController : BaseController
    {
        private IUsersBL _usersBL;

        public LoginController(IUsersBL usersBL, IHostEnvironment hostEnvironment, IModelMetadataProvider modelMetadataProvider, SiteConfiguration siteConfiguration) : base(hostEnvironment, modelMetadataProvider, siteConfiguration)
        {
            _usersBL = usersBL;
        }

        #region Login Logout

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            //await new CustomAuthenticationService().SignOutAsync(HttpContext);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync()
        {
            //if (ModelState.IsValid)
            //{
            //    LoginViewModelResponse validatUser = _usersBL.Validateuser(model.UserName, model.Password);
            //    if (validatUser != null)
            //    {
            //        await new CustomAuthenticationService().SignInUserAsync(validatUser, HttpContext);
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("UserName", "Credentials are not valid, Please try again.");
            //        return View("Index", model);
            //    }
            //}
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }

        #endregion Login Logout

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            //return View(new LoginViewModel());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPasswordPost()
        {
            ModelState["Password"].Errors.Clear();
            if (ModelState.IsValid)
            {
                //if (_usersBL.CheckEmail(model.UserName))
                //{
                //    _usersBL.GeneratePassword(model.UserName,CommonHelper.GenerateRandomDigitCode(6), _siteConfiguration.customConfiguration.CommonSettings.PasswordEncryptionSecurityKey);
                //}
                //else
                //{
                ModelState.AddModelError("UserName", "User not exist in the system.");
                return View("Index", null);
                //}
            }
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
    }
}