using Microsoft.EntityFrameworkCore;
using UserManagement.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using UserManagement.Models.DTOs;
using Azure;
public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    

    

    public UserRepository(UserDbContext context , IMapper mapper )
    {
        _context = context;
        _mapper = mapper;
       
    }

    public IEnumerable<User> GetAllUsers() =>_context.Users.ToList();

    public User GetUserById(int id)=> _context.Users.FirstOrDefault(u => u.Id == id);

    public User GetUserByEmail(string email) =>_context.Users.FirstOrDefault(u => u.Email == email);

    public void CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
    public Role GetRoleByName(string roleName)
    {
        return _context.Roles.FirstOrDefault(r => r.Name == roleName);
    }


    public void UpdateUser([FromBody] UserUpdateDto updatedUserDto)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Id == updatedUserDto.Id);

        if (existingUser == null)
            throw new Exception("User not found");

        // Check if the RoleId is valid
        bool roleExists = _context.Roles.Any(r => r.Id == updatedUserDto.RoleId);
        if (!roleExists)
            throw new Exception($"Role with ID {updatedUserDto.RoleId} does not exist");
        _mapper.Map(updatedUserDto, existingUser);

        _context.Users.Update(existingUser);
        _context.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            user.IsDeleted = true;
            
            _context.SaveChanges();
        }

    }


    //public async Task<List<User>> GetPagedUsers(int pageNumber, int pageSize)
    //{
    //    var pageParam = new SqlParameter("@PageNumber", pageNumber);
    //    var sizeParam = new SqlParameter("@PageSize", pageSize);

    //    return await _context.Users
    //        .FromSqlRaw("EXEC GetPagedUsers @PageNumber, @PageSize", pageParam, sizeParam)
    //        .ToListAsync();
    //}
    public List<User> GetPagedUsers(int PageNumber, int PageSize, bool IsDescending )
    {
        return _context.Users
            .FromSqlInterpolated($"EXEC GetPagedUsers @PageNumber = {PageNumber}, @PageSize = {PageSize}, @IsDescending = {IsDescending}")
            .ToList();
    }
    //public void UpdateUserRole(User user)
    //{
    //    _context.Users.Update(user);
    //    _context.SaveChanges();
    //}



    public Role GetRoleByRoleId(int Id)
    {
        var Role = _context.Roles.FirstOrDefault(r=>r.Id == Id);
        return Role;
    }
    
    public void UpdateUserRole(UpdateUserRoleDto updateUserRoleDto)
    {
        var existingUser = GetUserById(updateUserRoleDto.UserId);

        existingUser.RoleId = updateUserRoleDto.RoleId;

        _context.SaveChanges();
    }

    

    //public void UpdateUser(User user , int Id , UpdateUserRoleDto _role)
    //{
    //    var UpdateUser  = _role;

    //}

}
