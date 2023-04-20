using Architecture.BusinessLogic;
using Architecture.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Architechture.Web.Controllers
{
   // [ServiceFilter(typeof(LogConstantFilter))]
    //[ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class BaseController : Controller
    {
        public readonly IHostEnvironment _hostEnvironment;
        public readonly IModelMetadataProvider _modelMetadataProvider;
        public readonly SiteConfiguration _siteConfiguration;

        public BaseController(IHostEnvironment hostEnvironment, IModelMetadataProvider modelMetadataProvider, SiteConfiguration siteConfiguration)
        {
            _hostEnvironment = hostEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _siteConfiguration = siteConfiguration;
        }
    }
}
