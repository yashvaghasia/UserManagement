using UserManagement.Models.Entities;

public class Skill : BaseEntity
{
    public string Name { get; set; }
    public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
}
