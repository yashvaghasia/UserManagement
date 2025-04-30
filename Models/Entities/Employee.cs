using UserManagement.Models.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime JoiningDate { get; set; }
    public string Gender { get; set; } // M or F
    public int RoleId { get; set; }
    public Role Role { get; set; }

    public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    public ICollection<EmployeeHobby> EmployeeHobbies { get; set; }
}
