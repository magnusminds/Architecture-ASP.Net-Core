using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Infrastructure.Identity.Models
{
    public class SelectTenant
    {
        [Required]
        [DisplayName("Tenant")]
        public string TenantId { get; set; } = string.Empty;
    }
}
