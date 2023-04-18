using Architecture.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Architecture.Web.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Architechture.Web.Controllers
{
    [ServiceFilter(typeof(LogConstantFilter))]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class BaseController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public readonly SiteConfiguration _siteConfiguration;
        public BaseController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider, SiteConfiguration siteConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _siteConfiguration = siteConfiguration;
        }
    }
}
