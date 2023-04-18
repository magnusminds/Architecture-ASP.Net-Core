using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public int Id { get; set; }

        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public ApplicationRole(string roleName, int roleId) : base(roleName)
        {
            Id = roleId;
        }
    }
}
