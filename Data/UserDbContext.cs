using Microsoft.EntityFrameworkCore;
using UserManagement.Models.Entities;
public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<OtpEntry> OtpEntries { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
    public DbSet<EmployeeHobby> EmployeeHobbies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Role>().HasKey(r =>r.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmployeeSkill>()
            .HasKey(es => new { es.EmployeeId, es.SkillId });

        modelBuilder.Entity<EmployeeHobby>()
            .HasKey(eh => new { eh.EmployeeId, eh.HobbyId });
        modelBuilder.Entity<EmployeeSkill>()
      .HasOne(es => es.Employee)
      .WithMany(e => e.EmployeeSkills)
      .HasForeignKey(es => es.EmployeeId);

        modelBuilder.Entity<EmployeeSkill>()
            .HasOne(es => es.Skill)
            .WithMany(s => s.EmployeeSkills)
            .HasForeignKey(es => es.SkillId);

        modelBuilder.Entity<EmployeeHobby>()
            .HasOne(eh => eh.Employee)
            .WithMany(e => e.EmployeeHobbies)
            .HasForeignKey(eh => eh.EmployeeId);

        modelBuilder.Entity<EmployeeHobby>()
            .HasOne(eh => eh.Hobby)
            .WithMany(h => h.EmployeeHobbies)
            .HasForeignKey(eh => eh.HobbyId);
        //Seed Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 3, Name = "User" },
            new Role { Id = 2, Name = "Admin" },
            new Role { Id = 1, Name = "SuperAdmin" }
        );

        // Seed a SuperAdmin User
        modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "yash",
                    LastName = "Admin",
                    Email = "yash@example.com",
                    PasswordHash = "12345",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    CreatedAt = new DateTime(2024, 4, 1, 0, 0, 0),
                    UpdatedAt = new DateTime(2024, 4, 1, 0, 0, 0),
                    IsDeleted = false,
                    RoleId = 1 // SuperAdmin
                }
            );
    }
}

    //public List<User> GetPagedUsers(int PageNumber, int PageSize)
    //{
    //    return Users
    //        .FromSqlInterpolated($"EXEC GetPagedUsers1 @PageNumber = {PageNumber}, @PageSize = {PageSize}")
    //        .ToList();
    //}

