using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Models.DTOs;
using UserManagement.Models.Response;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    private readonly UserDbContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthController> _logger;
    private readonly IEmailService _emailService;
    public AuthController(IUserRepository userRepo, UserDbContext context, IConfiguration config, ILogger<AuthController> logger, IEmailService emailService)
    {
        _userRepo = userRepo;
        _context = context;
        _config = config;
        _logger = logger;
        _emailService = emailService;
    }
    //
    [HttpPost("register")]

    public IActionResult Register(RegisterDto request)
    {
        {
            _logger.LogInformation("Registration attempt for email: {Email}", request.Email);

            var existing = _userRepo.GetUserByEmail(request.Email);
            if (existing != null)
            {
                _logger.LogWarning("Registration failed: Email already exists - {Email}", request.Email);
                return BadRequest("Email already exists.");
            }


            var defaultRole = _userRepo.GetRoleByRoleId(3);
            if (defaultRole == null)
            {
                request.RoleId = defaultRole.Id;
            }
            User user = new User
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = request.Email.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                DateOfBirth = request.DateOfBirth,
                RoleId = request.RoleId,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            _userRepo.CreateUser(user);
         



            _emailService.SendEmail(
             to: request.Email,
              subject: "Registration Successful",
               body: $"Hi {request.FirstName},\n\nYou have successfully registered to our system.");



            _logger.LogInformation("User registered successfully: {Email}", request.Email);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "User registered successfully",
                Data = request.Email 
                  
            });
        

    }

    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto request)
    {
        _logger.LogInformation("Login attempt for email: {Email}", request.Email);

        var user = _userRepo.GetUserByEmail(request.Email);

        if (user == null)
        {
            _logger.LogWarning("Login failed: User not found for email: {Email}", request.Email);
            return BadRequest("Invalid Email or Password");
        }
        var Role = _userRepo.GetRoleByRoleId(user.RoleId);


        bool IsVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!IsVerified)
        {
            _logger.LogWarning("Login failed: Password mismatch for email: {Email}", request.Email);
            return BadRequest("Invalid Email or Password");
        }

        _logger.LogInformation("Login successful for email: {Email}", request.Email);

        var claims = new[]
        {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.Role, Role.Name)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationMinutes"])),
            signingCredentials: creds
        );

        return Ok(new  { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
    [HttpPut("update-role")]
    //[RequiresPermission("ManageUsers")]
    public IActionResult UapdateUserRole(UpdateUserRoleDto request)
    {
        var user = _userRepo.GetUserById(request.UserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var role = _userRepo.GetRoleByRoleId(request.RoleId);
        if (role == null)
        {
            return NotFound("Role not found.");
        }

        user.RoleId = request.RoleId;
        _userRepo.UpdateUserRole(request);

        return Ok(new ApiResponse<string>
        {
            Success = true,
            Message = "Role updated successfully",
            

        }) ;
    }
    [HttpPost("request-otp")]
    public IActionResult RequestOtp([FromBody] string email)
    {
        var user = _userRepo.GetUserByEmail(email);
        if (user == null)
            return NotFound("User not found");

        var otp = new Random().Next(100000, 999999).ToString();

        _context.OtpEntries.Add(new OtpEntry
        {
            Email = email,
            Otp = otp,
            ExpiryTime = DateTime.UtcNow.AddMinutes(5)
        });
        _context.SaveChanges();

        _emailService.SendEmail(email, "Password Reset OTP", $"Your OTP is: {otp}");

        return Ok(new ApiResponse<string>
        {
            Success = true,
            Message = "Mail Sent successfully",


        });
    }
   
    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var otpEntry = _context.OtpEntries.FirstOrDefault(x => x.Email == dto.Email && x.Otp == dto.Otp);
        if (otpEntry == null || otpEntry.ExpiryTime < DateTime.UtcNow)
            return BadRequest("Invalid or expired OTP.");

        var user = _userRepo.GetUserByEmail(dto.Email);
        if (user == null)
            return NotFound("User not found.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        _context.OtpEntries.Remove(otpEntry);
        _context.SaveChanges();

        return Ok(new ApiResponse<string>
        {
            Success = true,
            Message = "Password reset Successfully ",


        });
    }
    



}