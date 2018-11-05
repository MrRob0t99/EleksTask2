using System.Collections.Generic;
using System.Threading.Tasks;
using EleksTask.Models;

namespace EleksTask.Interface
{
    public interface IBasketRepository
    {
        Task<int> Add(string userId, int productId);

        Task<bool> Delete(string userId, int productId);

        Task<List<Product>> GetInfoProductAsync(string userId);
    }
}
