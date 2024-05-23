using Amazon.Runtime.Internal.Transform;
using Microsoft.AspNetCore.Mvc;
using OzPerksApi.Interfaces;
using OzPerksApi.Models;

namespace OzPerksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryService<User> _userService;
        private readonly ILogger _logger;
        public UserController(IRepositoryService<User> userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
           var users = await _userService.Get();
           _logger.LogInformation("Get all users");
           return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                await _userService.Create(user);
                _logger.LogInformation($"Added user {user.FullName}");
                return Ok(new { message = "A new user created" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(string Id,[FromBody]User user)
        {
            try
            {
                if (user != null)
                {
                    await _userService.Update(Id, user);
                    _logger.LogInformation($"Updated user {user.FullName}");
                    return Ok(new {message = $"{user.FullName} has been updated"});
                }
                else
                {
                    _logger.LogError("User not found");
                    return StatusCode(404, new { message = "User not found" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(string Id, [FromBody]User user)
        {
            try
            {
                if (Id != null)
                {
                    await _userService.Delete(Id);
                    _logger.LogInformation($"Deleted user {user.FullName}");
                    return Ok(new { message = "User has been deleted successfully" });
                }
                else
                {
                    _logger.LogError("User does not exists");
                    return StatusCode(404, new { message = "User does not exists" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
