namespace Architecture.Core.Constants
{
    public static class ApplicationIdentityConstants
    {
        public static readonly string DefaultPassword = "123Pa$$word!";
        public static readonly string TenantCookieName = "TenantId";
        public static readonly string TenantHeaderName = "TenantId";
        public static readonly string EncryptionSecret = "MAGNUSMINDS_SAB_KA_BAAP";
        public static class Roles
        {
            public static readonly string SuperAdmin = "SuperAdmin";
            public static readonly string Admin = "Admin";
            public static readonly string Customer = "Customer";

            public static readonly string[] RolesSupported = { SuperAdmin, Admin, Customer };
        }
        public static class Permissions
        {
            public static List<string> GeneratePermissionsForModule(string module)
            {
                return new List<string>()
                {
                    $"Permissions.{module}.Create",
                    $"Permissions.{module}.View",
                    $"Permissions.{module}.Update",
                    $"Permissions.{module}.Delete",
                };
            }

            public static List<string> GetAllPermissions()
            {
                return GeneratePermissionsForModule("User")
               .Union(GeneratePermissionsForModule("Roles"))
               .Union(GeneratePermissionsForModule("Customer"))
               .ToList();
            }

            public static bool CheckPermission(string permission)
            {
                return GetAllPermissions().Contains(permission);
            }

            public static bool CheckPermission(string permission, string module)
            {
                return GeneratePermissionsForModule(module).Contains(permission);
            }

            public static class Users
            {
                public const string View = "Permissions.Users.View";
                public const string Create = "Permissions.Users.Create";
                public const string Update = "Permissions.Users.Update";
                public const string Delete = "Permissions.Users.Delete";
            }

            public static class Roles
            {
                public const string View = "Permissions.Roles.View";
                public const string Create = "Permissions.Roles.Create";
                public const string Update = "Permissions.Roles.Update";
                public const string Delete = "Permissions.Roles.Delete";
            }


            public static class Customer
            {
                public const string View = "Permissions.Customer.View";
                public const string Create = "Permissions.Customer.Create";
                public const string Update = "Permissions.Customer.Update";
                public const string Delete = "Permissions.Customer.Delete";
            }
        }
    }
}
