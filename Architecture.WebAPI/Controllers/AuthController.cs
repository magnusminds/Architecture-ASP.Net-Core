using Architecture.BusinessLogic.UnitOfWork;
using Architecture.Core.Constants;
using Architecture.Core.Localizer.JsonString;
using Architecture.Dto;
using Architecture.Dto.APIResponse;

using Architecture.Infrastructure.Identity.Models;
using Architecture.Infrastructure.Services.Token;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Architecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IStringLocalizer<AuthController> _localizer;
        private readonly IUnitOfWorkBL _unitOfWorkBL;
        private readonly CurrentUser _currentUser;
        private readonly IMemoryCache _memoryCache;

        public AuthController(IStringLocalizer<AuthController> localizer, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IUnitOfWorkBL unitOfWorkBL, CurrentUser currentUser, IMemoryCache memoryCache)
        {
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
            _unitOfWorkBL = unitOfWorkBL;
            _currentUser = currentUser;
            _memoryCache = memoryCache;
        }


        [HttpGet("GetPermissions")]
        [Authorize]
        public async Task<ApiResponse> GetPermissionsAPI(CancellationToken cancellationToken)
        {
            var cacheString = "ARCPermissionDetails" + _currentUser.UserId + _currentUser.TenantId;
            if (_memoryCache.Get(cacheString) != null)
                return new ApiResponse(message: "Data found", result: _memoryCache.Get(cacheString), statusCode: 200);

            var allPermissions = ApplicationIdentityConstants.Permissions.GetAllPermissions();
            var rolePermissionResponseDto = await _unitOfWorkBL.RolePermissionBL.GetRolePermissions(Convert.ToString(_currentUser.RoleId), allPermissions, cancellationToken);
            var appPortalDetails = rolePermissionResponseDto.ToList();
            if (appPortalDetails == null)
            {
                return new ApiResponse(message: "No Data found", result: appPortalDetails, statusCode: 404);
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(12));
            _memoryCache.Set(cacheString, appPortalDetails, cacheOptions);

            return new ApiResponse(message: "Data found", result: appPortalDetails, statusCode: 200);
        }

        [HttpPost("AuthenticateAPI")]
        public async Task<APIResponse> AuthenticateAPI(TokenAPIRequest request, CancellationToken cancellationToken)
        {
            TokenResponse response = await _tokenService.AuthenticateAPI(request, cancellationToken);
            if (response != null)
            {
                var cacheString = "ARCPermissionDetails" + response.Id + response.TenantId;
                if (_memoryCache.Get(cacheString) != null)
                    _memoryCache.Remove(cacheString);

                return new APIResponse(messageCode: JsonStringResourcesKeys.TokenGeneratedSuccessfully, message: _localizer[JsonStringResourcesKeys.TokenGeneratedSuccessfully], result: response, statusCode: 200);
            }
            else
            {
                return new APIResponse(messageCode: JsonStringResourcesKeys.OTPInvalid, message: _localizer[JsonStringResourcesKeys.OTPInvalid], result: null, statusCode: 404);
            }
        }


        [HttpPost("GenerateOTPForAPI")]
        public async Task<APIResponse> GenerateOTPForAPI(TokenOtpGenerateRequest request, CancellationToken cancellationToken)
        {
            await _tokenService.GenerateOTP(request, cancellationToken);
            return new APIResponse(messageCode: JsonStringResourcesKeys.GenerateOTPSuccessfully, message: _localizer[JsonStringResourcesKeys.GenerateOTPSuccessfully], result: string.Empty, statusCode: 200);
        }


        [HttpPost("Logout")]
        [Authorize]
        public async Task<ApiResponse> Logout(CancellationToken cancellationToken)
        {
            var cacheString = "ARCPermissionDetails" + _currentUser.UserId + _currentUser.TenantId;
            if (_memoryCache.Get(cacheString) != null)
                _memoryCache.Remove(cacheString);

            return new ApiResponse(message: "Logout successful.!", result: null, statusCode: 200);
        }


        [HttpPost("login")]
   
        public async Task<APIResponse> Authenticate(TokenRequest request, CancellationToken cancellationToken)
        {
            string remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            TokenResponse response =  await _tokenService.Authenticate(request, remoteIpAddress, cancellationToken);
            if (response != null)
            {
                var cacheString = "ARCAuthDetails" + response.Id + response.TenantId;
                if (_memoryCache.Get(cacheString) != null)
                    _memoryCache.Remove(cacheString);

                return new APIResponse(messageCode: JsonStringResourcesKeys.TokenGeneratedSuccessfully, message: _localizer[JsonStringResourcesKeys.TokenGeneratedSuccessfully], result: response, statusCode: 200);
            }
            else
            {
                return new APIResponse(messageCode: JsonStringResourcesKeys.OTPInvalid, message: _localizer[JsonStringResourcesKeys.OTPInvalid], result: null, statusCode: 404);
            }

        }

       






    }
}
