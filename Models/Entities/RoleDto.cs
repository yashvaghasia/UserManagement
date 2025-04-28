
namespace UserManagement.Models.Dto.Role
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
        public bool IsDeleted { get; internal set; }
    }
}
