using Microsoft.EntityFrameworkCore;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<OtpEntry> OtpEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Role>().HasKey(r => r.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        // Seed Roles
        //modelBuilder.Entity<Role>().HasData(
        //    new Role { id = 6, Name = "User" },
        //    new Role { id = 5, Name = "Admin" },
        //    new Role { id = 4, Name = "SuperAdmin" }
        //);

        //// Seed a SuperAdmin User
        //modelBuilder.Entity<User>().HasData(
        //        new User
        //        {
        //            Id = 1,
        //            FirstName = "yash",
        //            LastName = "Admin",
        //            Email = "yash@example.com",
        //            PasswordHash = "$2a$11$w8E.kjUdDHZc5YVfrXnVruvw3YVv4UxfOLy2evKz6tRaRbT5MkA3K",
        //            DateOfBirth = new DateTime(1990, 1, 1),
        //            CreatedAt = new DateTime(2024, 04, 01, 0, 0, 0, DateTimeKind.Utc),
        //            UpdatedAt = new DateTime(2024, 04, 01, 0, 0, 0, DateTimeKind.Utc),
        //            IsDeleted = false,
        //            RoleId = 4 // SuperAdmin
        //        }
        //    );
    }
}

    //public List<User> GetPagedUsers(int PageNumber, int PageSize)
    //{
    //    return Users
    //        .FromSqlInterpolated($"EXEC GetPagedUsers1 @PageNumber = {PageNumber}, @PageSize = {PageSize}")
    //        .ToList();
    //}

