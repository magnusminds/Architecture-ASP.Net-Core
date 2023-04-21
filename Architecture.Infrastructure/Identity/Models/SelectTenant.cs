using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Architecture.Infrastructure.Identity.Models
{
    public class SelectTenant
    {
        [Required]
        [DisplayName("Tenant")]
        public string TenantId { get; set; } = string.Empty;
    }
}
