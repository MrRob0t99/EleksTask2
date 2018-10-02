using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EleksTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public ProductController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        //public async Task<IActionResult> CreateProductAsync([FromRoute] int categoryId, [FromBody]CreateProductDto productDto)
        //{
        //    var category =await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        //    if (category == null)
        //        return BadRequest("Category not found");
        //    //var product =  

        //}

        [HttpDelete("{idCategory}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int idCategory)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == idCategory);
            if (category == null)
                return BadRequest();
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }
    }
}
