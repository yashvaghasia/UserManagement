using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Attributes;
using UserManagement.Models.DTOs;
using UserManagement.Models.Entities;
using UserManagement.Models.Response;

[Route("api/[controller]")]
[ApiController]

public class AdminController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public AdminController(IUserRepository userRepository)
    {
        
        _userRepository = userRepository;
    }

    [HttpGet]
    [RequiresPermission("ViewUsers")]
    public IActionResult GetAllUsers() => Ok(_userRepository.GetAllUsers());

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userRepository.GetUserById(id);
        return user != null ? Ok(user) : NotFound();
    }

    [HttpPut("{id}")]
    [RequiresPermission("ViewUsers")]
    public IActionResult UpdateUser( [FromBody] UserUpdateDto updatedUser )
    {
        var user = _userRepository.GetUserById(updatedUser.Id);
        if (user == null)
            return NotFound("User not found");
        
        _userRepository.UpdateUser(updatedUser);
        return Ok(new ApiResponse<string>
        {
            Success = true,
            Message = "User updated successfully",

        });
    }
    [HttpPut("update-role")]
    [RequiresPermission("ManageUsers")]
    public IActionResult UpdateUserRole(UpdateUserRoleDto request )
        {
            var user = _userRepository.GetUserById(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var role = _userRepository.GetRoleByRoleId(request.RoleId);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            user.RoleId = request.RoleId;
        _userRepository.UpdateUserRole(request);

        return Ok(new ApiResponse<string>
        {
            Success = true,
            Message = "Role updated successfully",


        });
    }
    [RequiresPermission("ManageUsers")]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id )
    {
        _userRepository.DeleteUser(id);
        return Ok(new ApiResponse<string>
        {
            Success = true,
            Message = "Deleted successfully",


        });
    }
}
