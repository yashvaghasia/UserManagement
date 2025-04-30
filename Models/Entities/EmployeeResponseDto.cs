public class EmployeeResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime JoiningDate { get; set; }
    public string RoleName { get; set; }
    public List<string> Skills { get; set; }
    public List<string> Hobbies { get; set; }
}
