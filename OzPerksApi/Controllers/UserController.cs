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
        public UserController(IRepositoryService<User> userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var users = await _userService.Get();
            return users.ToList();
        }
    }
}
