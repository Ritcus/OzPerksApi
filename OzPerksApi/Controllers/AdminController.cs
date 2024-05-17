using Microsoft.AspNetCore.Mvc;
using OzPerksApi.Interfaces;
using OzPerksApi.Models;

namespace OzPerksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRepositoryService<Admin> _adminService;
        private readonly ILogger _logger;
        public AdminController(IRepositoryService<Admin> adminService, ILogger<UserController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Admin>> Get()
        {
            _logger.LogInformation("Get all admins");
            return await _adminService.Get();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Admin admin)
        {
            try
            {
                await _adminService.Create(admin);
                _logger.LogInformation($"Added admin {admin.FullName}");
                return Ok("A new admin created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(string Id, [FromBody]Admin admin)
        {
            try
            {
                if (admin != null)
                {
                    await _adminService.Update(Id, admin);
                    _logger.LogInformation($"Updated admin {admin.FullName}");
                    return Ok($"{admin.FullName} has been updated");
                }
                else
                {
                    _logger.LogError("Admin not found");
                    return StatusCode(404, "User not found");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(string Id)
        {
            try
            {
                if (Id != null)
                {
                    await _adminService.Delete(Id);
                    _logger.LogInformation($"Deleted admin {Id}");
                    return Ok("Admin has been deleted successfully");
                }
                else
                {
                    _logger.LogError("Admin does not exists");
                    return StatusCode(404, "User not found");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }

    }
}
