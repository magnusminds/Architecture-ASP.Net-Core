using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;

namespace Architecture.DataAccess.Interface
{
    public interface IRoleDA
    {
        public Task<IQueryable<ApplicationRole>> GetRoles(CancellationToken cancellationToken);

        public Task<IQueryable<ApplicationRole>> GetAllRoles(CancellationToken cancellationToken);

        public Task<IdentityResult> CreateRole(ApplicationRole model);

        public Task<IdentityResult> UpdateRole(ApplicationRole model);
    }
}