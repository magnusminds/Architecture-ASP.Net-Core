namespace Architecture.Dto.APIResponse
{
    public class PermissionsAPIResponse
    {
        public string ModuleName { get; set; }
        public List<Permissions> Permissions { get; set; } = new List<Permissions>();
    }
}
