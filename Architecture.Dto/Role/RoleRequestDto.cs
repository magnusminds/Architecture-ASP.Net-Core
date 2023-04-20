using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Architecture.Dto.Role
{
    public class RoleRequestDto
    {
        public string Id { get; set; }

        [Required]
        [Remote("DuplicateRoleName", "Role", AdditionalFields = nameof(Id), ErrorMessage = "Name already exist. Please enter a different name.")]
        public string Name { get; set; }

        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public int? TenantId { get; set; }

        public bool IsDelete { get; set; }
    }
}
