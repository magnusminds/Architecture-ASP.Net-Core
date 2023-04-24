using System;
using System.Collections.Generic;

namespace Architecture.DataBase.DatabaseFirst.Models
{
    public partial class Users
    {
        public Users()
        {
            UserRole = new HashSet<UserRole>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int UserId { get; set; }
        public string? MobileDeviceId { get; set; }
        public string? RegisteredFCMToken { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
