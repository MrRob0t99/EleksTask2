using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EleksTask.Dto;
using EleksTask.Models;
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
        private readonly IMapper _mapper;
        public ProductController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProductAsync([FromRoute] int categoryId, [FromBody]CreateProductDto productDto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
                return BadRequest("Category not found");
            var product = _mapper.Map<Product>(productDto);
            product.Category = category;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
                return BadRequest();
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return Ok(products);
        }

        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByCategoryIdAsync([FromRoute]int categoryId)
        {
            var productList = await _context
                .Products
                .Where(p => p.CategoryId == categoryId)
                .Select(pr => new {pr.Id, pr.Name, pr.Price})
                .ToListAsync();

            return Ok(productList);
        }

        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct([FromRoute]int productId)
        {
            var product = _context.Products.AsNoTracking().FirstAsync(p => p.Id == productId);
            if (product == null)
                return BadRequest("Not found");
            return Ok(product);
        }
    }
}
