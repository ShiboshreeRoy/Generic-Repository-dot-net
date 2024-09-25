using GenericRepository.Model;
using GenericRepository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IRepository<Blog> _blogRepository;

        public BlogController(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var blogs = await _blogRepository.GetAllAsync();
            return Ok(blogs);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var blogs  = await _blogRepository.GetByIdAsync(id);
            if (blogs == null)
            {
                return NotFound();
            }
            return Ok(blogs);
        }

        [HttpPost]

        public  async Task<IActionResult> Post([FromBody] Request.BlogRequest blogRequest)
        {
            var newblog = new Blog();
            {
                newblog.Id = blogRequest.Id;
                newblog.Name = blogRequest.Name;
                newblog.Description = blogRequest.Description;
            }

            var createblogresponse = await _blogRepository.AddAsync(newblog);
            return Ok(createblogresponse);
        }

        [HttpPut("id")]

        public async Task<IActionResult> Put(int id, [FromBody] Request.BlogRequest blogRequest)
        {
            var blog = await _blogRepository.GetByIdAsync(id);

            if (blog == null)
            {
                return NotFound();
            }
            blog.Name = blogRequest.Name;
            blog.Description = blogRequest.Description;
            await _blogRepository.UpdateAsync(blog);
            return Ok(blog);
        }
        [HttpDelete("id")]

        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if(blog == null)
            {
                return NotFound();
            }
            await _blogRepository.DeleteAsync(blog);
            return Ok(blog);
        }
    }

}
