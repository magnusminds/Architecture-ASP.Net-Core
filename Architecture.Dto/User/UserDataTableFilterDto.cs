using Architecture.Dto.DataTable;
using Newtonsoft.Json;

namespace Architecture.Dto.User
{
    public class UserDataTableFilterDto : DataTableFilterDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("roleName")]
        public string RoleName { get; set; }

        [JsonProperty("isActive")]
        public string IsActive { get; set; }
    }
}