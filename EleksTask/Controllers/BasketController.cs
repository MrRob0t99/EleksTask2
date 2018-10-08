using System.Threading.Tasks;
using EleksTask.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EleksTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> AddProductToBasketAsync([FromQuery]string userId, [FromQuery] int productId)
        {
            var response = await _basketService.AddProductToBasketAsync(userId, productId);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetInfoProduct([FromRoute] string userId)
        {
            var response = await _basketService.GetInfoProductAsync(userId);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> DeleteProductFromBasketAsync([FromQuery] string userId,[FromQuery]int productId)
        {
            var response = await _basketService.DeleteProductFromBasketAsync(userId,productId);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
