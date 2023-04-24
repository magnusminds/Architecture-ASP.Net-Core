using Architecture.Dto.RolePermission;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Architecture.Dto.Role
{
    public class RoleRequestDto
    {
        public RoleRequestDto()
        {
            Permissions = new List<string>();
        }
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string NormalizedName { get; set; }
        public int? TenantId { get; set; }

        public bool IsDelete { get; set; }

        public List<string> Permissions { get; set; }
    }
}
