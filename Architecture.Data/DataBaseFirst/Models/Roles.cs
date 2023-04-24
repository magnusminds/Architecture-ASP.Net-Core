using System;
using System.Collections.Generic;


namespace Architecture.DataBase.DataBaseFirst.Models
{
    public partial class Roles
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public int? TenantId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
