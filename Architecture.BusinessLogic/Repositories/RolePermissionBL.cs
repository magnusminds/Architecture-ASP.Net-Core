using Architecture.BusinessLogic.Interface;
using Architecture.DataAccess.UnitOfWork;
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

        public async Task<IList<string>> GetRolePermissions(string roleId, List<string> allPermissions, CancellationToken cancellationToken)
        {
            var allClaimsByRole = await GetAllRoleClaimsByRole(roleId, cancellationToken);
            return allClaimsByRole.Select(x => x.Value).ToList();
        }

        public async Task CreateRoleClaim(RolePermissionRequestDto rolePermissionRequestDto, CancellationToken cancellationToken)
        {
            var applicationRole = await _roleManager.FindByIdAsync(rolePermissionRequestDto.RoleId);
            await _unitOfWorkDA.RolePermissionDA.CreateRolePermission(applicationRole, rolePermissionRequestDto.Permission, cancellationToken);
        }

        public async Task CreateListRoleClaim(List<string> appDetails,string roleId, CancellationToken cancellationToken)
        {
            var applicationRole = await _roleManager.FindByIdAsync(roleId);
            foreach (var item in appDetails)
            {
                await _unitOfWorkDA.RolePermissionDA.CreateRolePermission(applicationRole, item, cancellationToken);
            }
        }

        public async Task DeleteRoleClaim(RolePermissionRequestDto model, CancellationToken cancellationToken)
        {
            var applicationRole = await _roleManager.FindByIdAsync(model.RoleId);
            await _unitOfWorkDA.RolePermissionDA.DeleteRolePermission(applicationRole, model.Permission, cancellationToken);
        }

        #region Api Method

        public async Task<List<string>> GetPermissions(List<string> appDetails, CancellationToken cancellationToken)
        {
            List<string> permissionsAPIResponseList = new List<string>();
            //  var rolePermissionList = appDetails.Select(a => a.Permission).ToList();
            var rolePermissionList = appDetails.ToList();
            foreach (var item in rolePermissionList)
            {
                permissionsAPIResponseList.Add(item);
            }

          

            return permissionsAPIResponseList;
        }

        #endregion Api Method
    }
}
