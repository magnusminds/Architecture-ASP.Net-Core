using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
