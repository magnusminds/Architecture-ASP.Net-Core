using Architecture.Dto.RolePermission;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.Interface
{
    public interface IRolePermissionBL
    {
        public Task<IList<Claim>> GetAllRoleClaimsByRole(string applicationRole, CancellationToken cancellationToken);

        public Task<bool> GetRoleHasPermission(string applicationRole, string permission, CancellationToken cancellationToken);

        public Task<IList<string>> GetRolePermissions(string Id, List<string> allPermissions, CancellationToken cancellationToken);

        public Task CreateRoleClaim(RolePermissionRequestDto rolePermissionRequestDto, CancellationToken cancellationToken);

        public Task CreateListRoleClaim(List<string> allAppPermissions, string roleId, CancellationToken cancellationToken);

        public Task DeleteRoleClaim(RolePermissionRequestDto model, CancellationToken cancellationToken);

        public Task<List<string>> GetPermissions(List<string> allAppPermissions, CancellationToken cancellationToken);
    }
}