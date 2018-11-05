using System.Collections.Generic;
using System.Threading.Tasks;
using EleksTask.Models;

namespace EleksTask.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> GetById(int categoryId);
        Task<List<Category>> GetAll();
        Task<int> Add(Category entity);
        Task<bool> Delete(int id);
        Task<bool> Rename(int categoryId, string newName);
    }
}