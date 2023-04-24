using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace Architecture.DataAccess.Interface
{
    public interface IRolePermissionDA
    {
        public Task<IList<Claim>> GetRoleClaimsByRole(ApplicationRole applicationRole, CancellationToken cancellationToken);

        public Task<bool> GetRoleHasPermission(string applicationRole, string permission, CancellationToken cancellationToken);

        public Task<IdentityResult> CreateRolePermission(ApplicationRole applicationRole, string claimValue, CancellationToken cancellationToken);

        public Task<IdentityResult> DeleteRolePermission(ApplicationRole applicationRole, string claimValue, CancellationToken cancellationToken);
    }
}
