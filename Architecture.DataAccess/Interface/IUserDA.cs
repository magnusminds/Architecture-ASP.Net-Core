using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;

namespace Architecture.DataAccess.Interface
{
    public interface IUserDA
    {
        public Task<IQueryable<ApplicationUser>> GetUsers(CancellationToken cancellationToken);

        public Task<IQueryable<ApplicationUser>> GetAllUsers(CancellationToken cancellationToken);

        public Task<IQueryable<ApplicationRole>> GetRoles(CancellationToken cancellationToken);

        public Task<IQueryable<IdentityUserRole<string>>> GetAspNetUserRoles(CancellationToken cancellationToken);

        public Task<string> GetUserRoleId(string Id, CancellationToken cancellationToken);

        public Task<IQueryable<IdentityUserRole<string>>> GetUserRoleData(string Id, CancellationToken cancellationToken);

        public Task<IdentityResult> CreateUser(ApplicationUser model, string password);

        public Task<IdentityResult> UpdateUser(ApplicationUser model);
    }
}
