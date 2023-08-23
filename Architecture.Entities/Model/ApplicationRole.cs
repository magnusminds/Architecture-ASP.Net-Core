using Microsoft.AspNetCore.Identity;

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