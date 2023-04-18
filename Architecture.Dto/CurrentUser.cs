using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Dto
{
    public class CurrentUser
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string ProfilePic { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public bool IsMultipleTenant { get; set; }
    }
}
