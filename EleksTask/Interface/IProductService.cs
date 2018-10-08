using System.Collections.Generic;
using System.Threading.Tasks;
using EleksTask.Dto;

namespace EleksTask.Interface
{
    public interface IProductService
    {
        Task<Response<int>> CreateProductAsync(int categoryId, CreateProductDto productDto);

        Task<Response<bool>> DeleteProductAsync(int productId);

        Task<Response<List<ProductDto>>> GetAllProducts();

        Task<Response<List<ProductDto>>> GetProductsByCategoryIdAsync(int categoryId);

        Task<Response<ProductDto>> GetProduct(int productId);
    }
}
