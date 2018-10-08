using System.Threading.Tasks;
using EleksTask.Dto;
namespace EleksTask.Interface
{
    public interface IBasketService
    {
        Task<Response<bool>> AddProductToBasketAsync(string userId, int productId);

        Task<Response<BasketDto>> GetInfoProduct(string userId);
    }
}
