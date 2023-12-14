
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APItask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IRepository _repository;
        public CategoriesController(AppDbContext db,IRepository repository)
        {
            _db = db;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync();

            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category category=await _repository.GetByIdAsync(id);

            if(category is null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK,category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto categoryDto )
        {

            Category category = new Category
            {
                Name = categoryDto.Name,
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created,category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _repository.GetByIdAsync(id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Name = name;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id<=0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _repository.GetByIdAsync(id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            _repository.Delete(existed);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
