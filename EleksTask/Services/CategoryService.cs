using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EleksTask.Dto;
using EleksTask.Interface;
using EleksTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EleksTask.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<Response<List<GetAllCategoryDto>>> GetAllCategories()
        {
            var response = new Response<List<GetAllCategoryDto>>();
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            response.Data = _mapper.Map<List<GetAllCategoryDto>>(categories);
            return response;
        }

        public async Task<Response<bool>> RenameCategoryAsync(int categoryId,string newName)
        {
            var response = new Response<bool>();
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                response.Error = new Error("Category not found");
                return response;
            }
            category.Name = newName;
            await _context.SaveChangesAsync();
            response.Data = true;
            return response;
        }

    }
}
