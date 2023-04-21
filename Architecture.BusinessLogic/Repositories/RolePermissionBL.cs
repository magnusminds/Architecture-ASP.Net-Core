using Architecture.BusinessLogic.Interface;
using Architecture.DataAccess.UnitOfWork;
using Architecture.Dto.APIResponse;
using Architecture.Dto.RolePermission;
using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.Repositories
{
    public class RolePermissionBL : IRolePermissionBL
    {
        private readonly IUnitOfWorkDA _unitOfWorkDA;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolePermissionBL(IUnitOfWorkDA unitOfWorkDA, RoleManager<ApplicationRole> roleManager)
        {
            _unitOfWorkDA = unitOfWorkDA;
            _roleManager = roleManager;
        }

        public async Task<IList<Claim>> GetAllRoleClaimsByRole(string applicationRole, CancellationToken cancellationToken)
        {
            var roleByName = await _roleManager.FindByIdAsync(applicationRole);
            var roleClaims = await _unitOfWorkDA.RolePermissionDA.GetRoleClaimsByRole(roleByName, cancellationToken);
            return roleClaims;
        }

        public async Task<bool> GetRoleHasPermission(string applicationRole, string permission, CancellationToken cancellationToken)
        {
            return await _unitOfWorkDA.RolePermissionDA.GetRoleHasPermission(applicationRole, permission, cancellationToken);
        }

        public async Task<List<RolePermissionResponseDto>> GetRolePermissions(string Id, List<string> allPermissions, CancellationToken cancellationToken)
        {
            var allClaimsByRole = await GetAllRoleClaimsByRole(Id, cancellationToken);
            List<RolePermissionResponseDto> RolePermissionResponseDto = new List<RolePermissionResponseDto>();
            foreach (var item in allPermissions)
            {
                RolePermissionResponseDto moduleResponseDtoChild = RolePermissionResponseDto.FirstOrDefault(p => p.ApplicationType == item.Split(".")[1]);
                if (moduleResponseDtoChild == null)
                {
                    moduleResponseDtoChild = new RolePermissionResponseDto();
                    moduleResponseDtoChild.ApplicationType = item.Split(".")[1];
                    moduleResponseDtoChild.RolePermissionList = new List<PermissiongResponseDto>();
                    RolePermissionResponseDto.Add(moduleResponseDtoChild);
                }
                PermissiongResponseDto permissionResponseDto = new PermissiongResponseDto();
                permissionResponseDto.Id = item.Split(".")[1] + item.Split(".")[2];
                permissionResponseDto.Module = item.Split(".")[2];
                permissionResponseDto.PermissionType = item.Split(".")[2];
                permissionResponseDto.Permission = item;
                if (allClaimsByRole.Any(x => x.Value == item))
                    permissionResponseDto.IsChecked = "checked";
                moduleResponseDtoChild.RolePermissionList.Add(permissionResponseDto);
            }
            return RolePermissionResponseDto;
        }

        public async Task CreateRoleClaim(RolePermissionRequestDto rolePermissionRequestDto, CancellationToken cancellationToken)
        {
            var applicationRole = await _roleManager.FindByIdAsync(rolePermissionRequestDto.RoleId);
            await _unitOfWorkDA.RolePermissionDA.CreateRolePermission(applicationRole, rolePermissionRequestDto.Permission, cancellationToken);
        }

        public async Task DeleteRoleClaim(RolePermissionRequestDto model, CancellationToken cancellationToken)
        {
            var applicationRole = await _roleManager.FindByIdAsync(model.RoleId);
            await _unitOfWorkDA.RolePermissionDA.DeleteRolePermission(applicationRole, model.Permission, cancellationToken);
        }

        #region Api Method

        public async Task<List<PermissionsAPIResponse>> GetPermissions(List<RolePermissionResponseDto> appDetails, CancellationToken cancellationToken)
        {
            List<PermissionsAPIResponse> permissionsAPIResponseList = new List<PermissionsAPIResponse>();
            var rolePermissionList = appDetails.Select(a => a.RolePermissionList).ToList();
            var allModules = rolePermissionList[0].Select(a => a.Module).ToList();
            foreach (var item in appDetails[0].RolePermissionList.DistinctBy(x => x.Module))
            {
                PermissionsAPIResponse permissionsAPIResponse = new PermissionsAPIResponse();
                foreach (var item2 in appDetails[0].RolePermissionList.Where(x => x.Module == item.Module))
                {
                    permissionsAPIResponse.ModuleName = item.Module;
                    Permissions permissionAccess = new Permissions()
                    {
                        Feature = item2.Permission.Replace("Permissions.App." + item.Module + ".", "").Replace("Permissions.MfgApp." + item.Module + ".", ""),
                        HasAccess = item2.IsChecked == "checked" ? true : false
                    };
                    permissionsAPIResponse.Permissions.Add(permissionAccess);
                }
                permissionsAPIResponseList.Add(permissionsAPIResponse);
            }
            return permissionsAPIResponseList;
        }

        #endregion Api Method
    }
}
