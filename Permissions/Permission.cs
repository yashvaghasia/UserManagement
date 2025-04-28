namespace UserManagement.Permissions
{
    public static class Permissions
    {
        public static readonly Dictionary<string, List<string>> RolePermissions = new()
        {
            { "SuperAdmin", new List<string> { "UpdateRoles", "ManageUsers", "ViewUsers" } },
            { "Admin", new List<string> { "UpdateRoles", "ViewUsers" } },
            { "User", new List<string> { "ViewUsers" } }
        };
    }
}
