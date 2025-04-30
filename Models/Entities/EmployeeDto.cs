public class EmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime JoiningDate { get; set; }
    public string Gender { get; set; }
    public int RoleId { get; set; }
    public List<int> SkillIds { get; set; }
    public List<int> HobbyIds { get; set; }
}
