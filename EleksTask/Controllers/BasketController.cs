using System.Linq;
using System.Threading.Tasks;
using EleksTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EleksTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BasketController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProductToBasketAsync([FromQuery]string userName, [FromQuery] int productId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
                return BadRequest("User not found");
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
                return BadRequest("Product not found");
            var basketProduct = new BasketProduct()
            {
                ApplicationUser = user,
                Product = product
            };
            await _context.BasketProducts.AddAsync(basketProduct);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInfoProduct([FromRoute] string userId)
        {
            var productList = await _context
                .BasketProducts
                .Where(bp => bp.ApolicationuserId == userId)
                .Include(p=>p.Product)
                .Select(p => p.Product)
                .ToListAsync();

            return Ok(new {Products = productList,TotalPrice = productList.Sum(p=>p.Price)});
        }
    }
}
