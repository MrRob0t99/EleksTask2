﻿using System.Linq;
using System.Threading.Tasks;
using EleksTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EleksTask.Services
{
    public class CategoryService
    {
        private readonly ApplicationContext _context;

        public CategoryService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> CreateCategoryAsync(string name)
        {
            var response = new Response<int>();
            if (await _context.Categories.AnyAsync(c => c.Name == name))
            {
                response.Error = new Error($"Category with {name} already exist");
            }
            var newCategory = new Category()
            {
                Name = name
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            response.Data = newCategory.Id;
            return response;
        }

        public async Task<Response<bool>> DeleteCategoryAsync([FromRoute] int categoryId)
        {
            var response = new Response<bool>();
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                response.Error = new Error("Category not found");
            }
            _context.Remove(category);
            await _context.SaveChangesAsync();
            response.Data = true;
            return response;
        }
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }

        public async Task<IActionResult> RenameCategoryAsync([FromRoute]int categoryId, [FromBody]string newName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
                return BadRequest("Category not found");
            category.Name = newName;
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}

}
}