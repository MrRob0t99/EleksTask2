using System.Collections.Generic;
using System.Threading.Tasks;
using EleksTask.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EleksTask.Interface
{
    public interface ICategoryService
    {
        Task<Response<int>> CreateCategoryAsync(string name);

        Task<Response<bool>> DeleteCategoryAsync([FromRoute] int categoryId);

        Task<Response<List<GetAllCategoryDto>>> GetAllCategories();

        Task<Response<bool>> RenameCategoryAsync(int categoryId, string newName);
    }
}
