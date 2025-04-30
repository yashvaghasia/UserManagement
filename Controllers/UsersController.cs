using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using System.Numerics;
using UserManagement.Attributes;
using UserManagement.Models.Entities;
using UserManagement.Models;
using UserManagement.Models.Response;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserDbContext _context;
        

        public UserController(IUserRepository userRepository , UserDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
                return NotFound(ApiResponse<string>.ErrorResponse("User not found"));

            return Ok(ApiResponse<UserDto>.SuccessResponse(user));
        }


        //  Anyone authenticated (User, Admin, SuperAdmin) can see their own profile
        [RequiresPermission("ViewUsers")]
        [HttpGet("me")]
        public IActionResult GetMyProfile()
        {
            var email = User.Identity?.Name;
            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }
       
        // Only Admin and SuperAdmin can register new users
        [RequiresPermission("ManageUsers")]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {
            var existingUser = _userRepository.GetUserByEmail(model.Email);
            if (existingUser != null)
                return BadRequest("Email already exists");

            model.CreatedAt = DateTime.Now;
            _userRepository.CreateUser(model);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Role updated successfully",
                Data = model.Email

            });
        }

        // Admin and SuperAdmin can view all users
        [RequiresPermission("ViewUsers")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        // Admin and SuperAdmin can update users
        [RequiresPermission("UpdateRoles")]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id , [FromBody] UserUpdateDto updatedUser)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
                return NotFound("User not found");
            user.Id = id;
            _userRepository.UpdateUser(updatedUser);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "User updated successfully",


            });
        }

        // Admin and SuperAdmin can delete users
        //[RequiresPermission("ManageUsers")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user != null)
                
                
              _userRepository.DeleteUser(id);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "User deleted successfully",


            });
        }

        //  SuperAdmin-only endpoint
        [RequiresPermission("ManagaeUsers")]
        [HttpGet("superadmin-panel")]
        public IActionResult SuperAdminOnlyEndpoint()
        {
            return Ok("Welcome, SuperAdmin!");
        }

        [HttpPost("paginated")]
        public IActionResult GetUsersWithPagination([FromBody] PaginationRequestDto request , UserDbContext context)
        {
            var result = _userRepository.GetPagedUsers(request.PageNumber, request.PageSize, request.IsDescending);
            int count = _context.Users.Count();


            return Ok(new
            {
                TotalCount = count,
                UserList = result
            }) ;
        }
        

        
        
    }
}
