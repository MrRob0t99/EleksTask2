using System.Collections.Generic;
using System.Threading.Tasks;
using EleksTask.Models;
namespace EleksTask.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetById(int categoryId);
        Task<List<Product>> GetProductsByCategoryId(int categoryId,int skip,int take, string search);
        Task<List<Product>> GetAll();
        Task<int> Add(int categoryId, Product entity);
        Task<bool> Delete(int id);
        Task<bool> Rename(int categoryId, string newName);
    }
}
