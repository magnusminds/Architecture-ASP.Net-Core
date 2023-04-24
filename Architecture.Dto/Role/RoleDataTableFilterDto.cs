using Architecture.Dto.DataTable;
using Newtonsoft.Json;


namespace Architecture.Dto.Role
{
    public class RoleDataTableFilterDto : DataTableFilterDto
    {
        [JsonProperty("roleName")]
        public string RoleName { get; set; }
    }
}
