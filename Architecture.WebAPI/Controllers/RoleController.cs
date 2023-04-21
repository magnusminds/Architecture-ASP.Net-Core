using Architecture.BusinessLogic.UnitOfWork;
using Architecture.Dto;
using Architecture.Dto.Role;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Architecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWorkBL _unitOfWorkBL;
      
        private readonly CurrentUser _currentUser;
        

        public RoleController(IUnitOfWorkBL unitOfWorkBL,IStringLocalizer<RoleController> localizer, CurrentUser currentUser) 
        {
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

        public async Task<IActionResult> GetRoleData([FromForm] RoleDataTableFilterDto dataTableFilterDto, CancellationToken cancellationToken)
        {
            var data = await _unitOfWorkBL.RoleBL.GetFilterRoleData(dataTableFilterDto, cancellationToken);
            return Ok(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleRequestDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> CreateRole(RoleRequestDto roleRequestDto, CancellationToken cancellationToken)
        {
            var data = await _unitOfWorkBL.RoleBL.CreateRole(roleRequestDto, cancellationToken);
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
