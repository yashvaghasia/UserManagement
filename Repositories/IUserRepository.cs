using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models.DTOs;
using UserManagement.Models.Entities;

public interface IUserRepository
{
    //public (int TotalCount, List<UserDto> Users) GetPaginatedUsers(PaginationRequestDto request);
    public List<User> GetPagedUsers(int PageNumber, int PageSize, bool IsDescending);
   

   
    IEnumerable<User> GetAllUsers();
    User GetUserById(int id);
    User GetUserByEmail(string email);
    void CreateUser(User user);
   public void DeleteUser(int id);
   public void UpdateUser(UserUpdateDto updatedUserDto );
    Role GetRoleByRoleId(int Id);
   public void UpdateUserRole(UpdateUserRoleDto updateUserRoleDto);
    
}
