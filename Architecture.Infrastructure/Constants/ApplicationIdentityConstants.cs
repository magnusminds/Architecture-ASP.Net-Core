using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Infrastructure.Constants
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
            public static readonly string Basic = "Basic";
            public static readonly string Admin = "Admin";

            public static readonly string[] RolesSupported = { SuperAdmin, Basic, Admin };
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
               .Union(GeneratePermissionsForModule("Image")).ToList();
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

            public static class Image
            {
                public const string View = "Permissions.Image.View";
                public const string Create = "Permissions.Image.Create";
                public const string Update = "Permissions.Image.Update";
                public const string Delete = "Permissions.Image.Delete";
            }


        }
    }
}
