using System.Threading.Tasks;
using EleksTask.Dto;
using EleksTask.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EleksTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProductAsync([FromRoute] int categoryId, [FromBody]CreateProductDto productDto)
        {
            var response = await _productService.CreateProductAsync(categoryId, productDto);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int productId)
        {
            var response = await _productService.DeleteProductAsync(productId);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByCategoryIdAsync([FromRoute]int categoryId)
        {
            var response = await _productService.GetProductsByCategoryIdAsync(categoryId);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct([FromRoute]int productId)
        {
            var response = await _productService.GetProduct(productId);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
