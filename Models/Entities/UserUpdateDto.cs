using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Entities
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
         public string FirstName { get; set; }
         public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
    }
}
