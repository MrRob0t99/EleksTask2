using System.Threading.Tasks;
using EleksTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EleksTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CategoryController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody]string name)
        {

            if (await _context.Categories.AnyAsync(c => c.Name == name))
            {
                return BadRequest($"Category with {name} already exist");
            }
            var newCategory = new Category()
            {
                Name = name
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return Ok(newCategory);
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
                return BadRequest();
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }

        [HttpPut("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RenameCategoryAsync([FromRoute]int categoryId, [FromBody]string newName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
                return BadRequest("Category not found");
            category.Name = newName;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPrductsByCategoryIdAsync([FromRoute]int categoryId)
        {
            var category = await _context.Categories.Include(c=>c.Products).FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
                return BadRequest("Category not found");
            return Ok(category);
        }
    }
}
