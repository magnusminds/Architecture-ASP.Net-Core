using Architecture.BusinessLogic.UnitOfWork;
using Architecture.Core.Constants;
using Architecture.Dto;
using Architecture.Dto.Role;
using Architecture.Dto.RolePermission;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Architecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWorkBL _unitOfWorkBL;
        public readonly IStringLocalizer<RoleController> _localizer;
        private readonly CurrentUser _currentUser;


        public RoleController(IUnitOfWorkBL unitOfWorkBL, IStringLocalizer<RoleController> localizer, CurrentUser currentUser)
        {
            _localizer = localizer;
            _unitOfWorkBL = unitOfWorkBL;
            _currentUser = currentUser;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTableFilterDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetRoleData")]
        [Authorize(ApplicationIdentityConstants.Permissions.Roles.View)]
        public async Task<ApiResponse> GetRoleData(RoleDataTableFilterDto dataTableFilterDto, CancellationToken cancellationToken)
        {
            var data = await _unitOfWorkBL.RoleBL.GetFilterRoleData(dataTableFilterDto, cancellationToken);
            if (data == null)
            {
                return new ApiResponse(message: "No Data found", result: null, statusCode: 500);
            }
            return new ApiResponse(message: "Data Found", result: null, statusCode: 200);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleRequestDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(ApplicationIdentityConstants.Permissions.Roles.Create)]
        public async Task<ApiResponse> CreateRole(RoleRequestDto roleRequestDto, CancellationToken cancellationToken)
        {
            var data = await _unitOfWorkBL.RoleBL.CreateRole(roleRequestDto, cancellationToken);
            var roleData = await _unitOfWorkBL.RoleBL.GetAllRoles(cancellationToken);
            var insertedRoleData = roleData.Where(x => x.Name == roleRequestDto.Name).FirstOrDefault();
            string roleId = insertedRoleData.Id;
            await _unitOfWorkBL.RolePermissionBL.CreateListRoleClaim(roleRequestDto.Permissions, roleId, cancellationToken);
            if (data == null)
            {
                return new ApiResponse(message: "Internal server error", result: null, statusCode: 500);
            }
            return new ApiResponse(message: "Data inserted successful", result: null, statusCode: 200);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleRequestDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("UpdateRole")]
        [Authorize(ApplicationIdentityConstants.Permissions.Roles.Update)]
        public async Task<ApiResponse> UpdateRole(RoleRequestDto roleRequestDto, CancellationToken cancellationToken)
        {
            var data = await _unitOfWorkBL.RoleBL.UpdateRole(roleRequestDto, cancellationToken);
            if (data == null)
            {
                return new ApiResponse(message: "Internal server error", result: null, statusCode: 500);
            }
            return new ApiResponse(message: "Data updated successful", result: null, statusCode: 200);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("Role/{roleId}")]
        [Authorize(ApplicationIdentityConstants.Permissions.Roles.Delete)]
        public async Task<ApiResponse> DeleteRole(string Id, CancellationToken cancellationToken)
        {
            var data = await _unitOfWorkBL.RoleBL.DeleteRole(Id, cancellationToken);
            if (data == null)
            {
                return new ApiResponse(message: "Internal server error", result: null, statusCode: 500);
            }
            return new ApiResponse(message: "Data delete successful", result: null, statusCode: 200);
        }


    }
}
