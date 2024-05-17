
using Microsoft.AspNetCore.Mvc;
using OzPerksApi.Interfaces;
using OzPerksApi.Models;
using static OzPerksApi.Models.Enum.Enums;

namespace OzPerksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IRepositoryService<Post> _postService;
        private readonly ILogger _logger;
        public PostController(IRepositoryService<Post> postService, ILogger<PostController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        [HttpGet("type/{postType}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByType(string postType)
        {
            PostType type;
            try
            { 
                if (!Enum.TryParse(postType.ToLower(), out type))
                {
                    return BadRequest("Invalid post type.");
                }

            }
            catch {

                return BadRequest("Invalid post type.");
            }
            var posts = await _postService.GetPostsByType(type);
            return Ok(posts);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm]Post post, IFormFile file)
        {
            try
            {
                var imageData = _postService.ConveryImageToByteArray(file);
                post.Image = imageData.Result;
                await _postService.Create(post);
                _logger.LogInformation("A new post has been created");
                return Ok("A new post has been created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Post>> GetPostByIdAsync(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    _logger.LogError("Post not found.");
                    return BadRequest("Post not found.");
                }
                var post = await _postService.GetByIdAsync(Id);
                return Ok(post);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAsync(string Id, [FromBody] Post post)
        {
            try
            {
                if (string.IsNullOrEmpty(Id) || post == null)
                {
                    _logger.LogError("Post not found.");
                    return BadRequest("Post not found.");
                }
                await _postService.Update(Id, post);
                _logger.LogInformation($"A post with id `{Id}` has been updated");
                return Ok($"A post with id `{Id}` has been updated");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAsync(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    _logger.LogError("Post not found.");
                    return BadRequest("Post not found.");
                }
                await _postService.Delete(Id);
                _logger.LogInformation($"A post with id `{Id}` has been deleted");
                return Ok($"A post with id `{Id}` has been deleted");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}