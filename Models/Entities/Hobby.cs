using UserManagement.Models.Entities;

public class Hobby : BaseEntity
{
    public string Name { get; set; }
    public ICollection<EmployeeHobby> EmployeeHobbies { get; set; }
}
