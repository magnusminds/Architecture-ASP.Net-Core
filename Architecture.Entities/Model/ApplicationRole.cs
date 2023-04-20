using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Entities.Model
{
    public class ApplicationRole : IdentityRole
    {
        public int TenantId { get; set; }
        public bool IsDeleted { get; set; }

        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public ApplicationRole(string roleName, int tenantId) : base(roleName)
        {
            TenantId = tenantId;
        }
    }
}
