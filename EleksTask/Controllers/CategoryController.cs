using System.Collections.Generic;
using System.Threading.Tasks;
using EleksTask.Dto;
using EleksTask.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EleksTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody]string name)
        {
            var response = await _categoryService.CreateCategoryAsync(name);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int categoryId)
        {
            var response = await _categoryService.DeleteCategoryAsync(categoryId);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int categoryId)
        {
            var response = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategories();
            if (response.Error != null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RenameCategoryAsync([FromRoute]int categoryId, [FromBody]string newName)
        {
            var response = await _categoryService.RenameCategoryAsync(categoryId, newName);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
