using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using TaskScheduler.Agent.Interfaces;
using TaskScheduler.Entities;
using TaskScheduler.Helper.Interfaces;

namespace TaskScheduler.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAgent _userAgent;
        private readonly IJwtUtils _jwtUtils;
        public UserController(IUserAgent userAgent,IJwtUtils jwtUtils)
        {
            _userAgent = userAgent;   
            _jwtUtils = jwtUtils;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> login([FromBody] UserDto user)
        {
            if (user==null)
            {
                return BadRequest(new ApiResponse<UserDto>()
                {
                    Status = "failure",
                    Message = "Invalid login credentials",
                    Data = null
                });
            }
            else
            {
                var response = await _userAgent.Login(user);
                if (response.Status != "success")
                {
                    return Unauthorized(response);

                }
                else
                {
                    string token= _jwtUtils.GenerateJwtToken(response.Data);
                    UserDto userDto = new UserDto()
                    {
                        Email = response.Data.Email,
                        Username = response.Data.Username,
                        Token = token
                    };
                    return Ok(new ApiResponse<UserDto>()
                    {
                        Status = response.Status,
                        Message = response.Message,
                        Data=userDto
                    });
                }
                
            }
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetUsers()
        {
           var response = await _userAgent.GetUsers();
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else if (response.Status == "failure")
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {

            var response= await _userAgent.CreateUser(new User()
            {
                Email= user.Email,
                PasswordHash=user.Password,
                Username=user.Username
            });
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else if(response.Status =="failure")
            {
                return BadRequest(response);
            }
            else
            {
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result= await _userAgent.GetUserById(id);
            if (result.Status == "success")
            {
                return Ok(result);
            }
            else if(result.Status == "failure")
            {
                return NotFound(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response=await _userAgent.DeleteUser(id);
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else if (response.Status == "failure")
            {
                return BadRequest(response);
            }
            else
            {
                return StatusCode(500, response);
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var response= await _userAgent.UpdateUser(user);
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else if(response.Status == "failure")
            {
                return BadRequest(response);
            }
            else
            {
                return StatusCode(500, response);
            }
        }

    }
}
