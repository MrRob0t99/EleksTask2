using System.Collections.Generic;
using System.Threading.Tasks;
using EleksTask.Dto;
using EleksTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace EleksTask.Interface
{
    public interface IProduct
    {
        Task<Response<int>> CreateProductAsync(int categoryId, CreateProductDto productDto);

        Task<Response<bool>> DeleteProductAsync(int productId);

        Task<Response<List<Product>>> GetAllProducts();

        Task<Response<List<Product>>> GetProductsByCategoryIdAsync(int categoryId);

        Task<Response<Product>> GetProduct(int productId);
    }
}
