using Architechture.Web.Controllers;
using Architecture.BusinessLogic.Interface;
using Architecture.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Architecture.Web.Controllers
{
    public class UsersController : BaseController
    {
        private IUsersBL _usersBL;

        public UsersController(IUsersBL usersBL, IHostEnvironment hostEnvironment, IModelMetadataProvider modelMetadataProvider, SiteConfiguration siteConfiguration) : base(hostEnvironment, modelMetadataProvider, siteConfiguration)
        {
            _usersBL = usersBL;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}