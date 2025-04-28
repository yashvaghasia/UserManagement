using System.ComponentModel.DataAnnotations;
using System.Data;
using UserManagement.Models.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int RoleId { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
}

