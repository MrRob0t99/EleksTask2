using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EleksTask.Dto;
using EleksTask.Interface;
using EleksTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace EleksTask.Services
{
    public class CategoryService :ICategoryService
    {

        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public CategoryService(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Response<int>> CreateCategoryAsync(string name)
        {
            var response = new Response<int>();
            var newCategory = new Category()
            {
                Name = name
            };
            var res = await _repository.Add(newCategory);
            if (res == -1)
            {
                response.Error = new Error($"Category with {name} already exist");
                return response;
            }
            response.Data = res;
            return response;
        }

        public async Task<Response<bool>> DeleteCategoryAsync([FromRoute] int categoryId)
        {
            var response = new Response<bool>();
            var result = await _repository.Delete(categoryId);
            if (!result)
            {
                response.Error = new Error("Category not found");
            }
            response.Data = result;
            return response;
        }

        public async Task<Response<List<GetAllCategoryDto>>> GetAllCategories()
        {
            var response = new Response<List<GetAllCategoryDto>>();
            var categories = await _repository.GetAll();
            response.Data = _mapper.Map<List<GetAllCategoryDto>>(categories);
            return response;
        }

        public async Task<Response<GetCategoryDto>> GetCategoryByIdAsync(int categoryId)
        {
            var response = new Response<GetCategoryDto>();
            var category =await _repository.GetById(categoryId);
            if (category == null)
            {
                response.Error = new Error("Category not found");
                return response;
            }
            response.Data = _mapper.Map<GetCategoryDto>(category);
            return response;
        }

        public async Task<Response<bool>> RenameCategoryAsync(int categoryId,string newName)
        {
            var response = new Response<bool>();
            var result = await _repository.Rename(categoryId,newName);
            if (!result)
            {
                response.Error = new Error("Category not found");
                return response;
            }
            response.Data = true;
            return response;
        }

    }
}
