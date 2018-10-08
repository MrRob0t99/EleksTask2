using System.Threading.Tasks;
using EleksTask.Dto;
namespace EleksTask.Interface
{
    public interface IBasketService
    {
        Task<Response<bool>> AddProductToBasketAsync(string userId, int productId);

        Task<Response<BasketDto>> GetInfoProductAsync(string userId);

        Task<Response<bool>> DeleteProductFromBasketAsync(string userId, int productId);
    }
}
