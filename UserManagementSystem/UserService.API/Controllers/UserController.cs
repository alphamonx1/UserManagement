using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserService.Application.DTOs;
using UserService.Application.Services;
using UserService.Domain.Entities;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [SwaggerOperation(Summary = "Login", Description = "Login")]
        [SwaggerResponse(200, "Login Successfull", typeof(User))]
        [SwaggerResponse(400, "Login Failed")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var user = await _userService.Authenticate(loginRequest.Username, loginRequest.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var token = _userService.GenerateJwtToken(user);
            return Ok(new { token });
        }

        [SwaggerOperation(Summary = "User Register", Description = "User register")]
        [SwaggerResponse(200, "User register successfully", typeof(User))]
        [SwaggerResponse(400, "User register unsuccessfully")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
        {
            try
            {
                var success = await _userService.RegisterUser(registerRequest.Username, registerRequest.Password, registerRequest.Fullname);
                if (!success)
                {
                    return BadRequest(new { message = "Username already exists" });
                }
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message }); 
            }
        }

        [SwaggerOperation(Summary = "Get User information", Description = "get user information ")]
        [SwaggerResponse(200, "User Information Found", typeof(User))]
        [SwaggerResponse(400, "Invalid request data")]
        [SwaggerResponse(401, "Unauthorized")]
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetUserInformation([FromQuery] Guid userId)
        {
            var userInformation = await _userService.GetUserInformationAsync(userId);
            if (userInformation == null)
            {
                return NotFound();
            }
            return Ok(userInformation);
        }
    }
}
